using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class VendingMachine
{
    private decimal _currentAmount;
    private Product _selectedProduct;
    private readonly List<Tray> _trays;
    private readonly BoxWithMoney _moneyBox;
    private readonly Display _display;
    private readonly Lock _lock;
    private readonly ProductContext _context;
    private System.Windows.Forms.Timer _idleTimer;
    private System.Windows.Forms.Timer _displayResetTimer;
    private System.Windows.Forms.Timer _messageTimer;
    private string _temporaryMessage;
    private bool _showingTemporaryMessage;

    public string DisplayText
    {
        get
        {
            if (_showingTemporaryMessage)
                return _temporaryMessage;
            else
                return _display.Text;
        }
    }

    public List<Tray> Trays => _trays;
    public decimal CurrentAmount => _currentAmount;
    public Product SelectedProduct => _selectedProduct;

    // События
    public event Action DisplayUpdated;
    public event Action<string> StationNotification;
    public event Action AdminRequested;
    public event Action<int, bool> TrayStatusChanged;

    public VendingMachine()
    {
        _context = new ProductContext();
        _display = new Display();
        _lock = new Lock();
        _moneyBox = new BoxWithMoney();
        _trays = new List<Tray>();
        _showingTemporaryMessage = false;

        InitializeTrays();
        SetupTimers();
    }

    private void InitializeTrays()
    {
        var products = _context.Products.ToList();

        for (int i = 1; i <= 8; i++)
        {
            var tray = new Tray(i);
            var product = products.FirstOrDefault(p => p.SlotNumber == i);

            if (product != null)
            {
                tray.Product = product;
                TrayStatusChanged?.Invoke(i - 1, tray.IsEmpty);
            }

            _trays.Add(tray);
        }
    }

    private void SetupTimers()
    {
        _idleTimer = new System.Windows.Forms.Timer();
        _idleTimer.Interval = 60000;
        _idleTimer.Tick += (s, e) => ReturnMoney();

        _displayResetTimer = new System.Windows.Forms.Timer();
        _displayResetTimer.Interval = 60000;
        _displayResetTimer.Tick += (s, e) => ResetDisplay();

        _messageTimer = new System.Windows.Forms.Timer();
        _messageTimer.Interval = 3000;
        _messageTimer.Tick += (s, e) => ClearTemporaryMessage();
    }

    public void InsertMoney(decimal amount)
    {
        ResetIdleTimer();
        ResetDisplayTimer();

        _currentAmount += amount;
        _moneyBox.Insert(amount);

        if (!_showingTemporaryMessage)
        {
            _display.UpdateDisplay(_currentAmount, _selectedProduct);
            DisplayUpdated?.Invoke();
        }
    }

    public void SelectSlot(int slotNumber)
    {
        ResetIdleTimer();
        ResetDisplayTimer();

        var tray = _trays.FirstOrDefault(t => t.Number == slotNumber);
        if (tray != null)
        {
            _selectedProduct = tray.Product;

            if (tray.IsEmpty)
            {
                // СООБЩЕНИЕ: Товар закончился
                ShowTemporaryMessage("ТОВАР ЗАКОНЧИЛСЯ!", 2000);
                _selectedProduct = null;
            }
            else
            {
                if (!_showingTemporaryMessage)
                {
                    _display.UpdateDisplay(_currentAmount, _selectedProduct);
                    DisplayUpdated?.Invoke();
                }
            }
        }
    }

    public void Purchase()
    {
        ResetIdleTimer();
        ResetDisplayTimer();

        if (_selectedProduct == null)
        {
            ShowTemporaryMessage("ВЫБЕРИТЕ ТОВАР!", 2000);
            return;
        }

        if (_currentAmount < _selectedProduct.Price)
        {
            // СООБЩЕНИЕ: Недостаточно средств
            ShowTemporaryMessage($"НЕДОСТАТОЧНО СРЕДСТВ!\nНУЖНО: {_selectedProduct.Price}₽", 3000);
            return;
        }

        ProcessPurchase();
    }

    private void ProcessPurchase()
    {
        var tray = _trays.FirstOrDefault(t => t.Product == _selectedProduct);
        if (tray == null || tray.IsEmpty) return;

        // Уменьшаем количество
        tray.Product.Quantity--;
        _context.SaveChanges();

        // Обновляем статус лампочки
        TrayStatusChanged?.Invoke(tray.Number - 1, tray.IsEmpty);

        // СООБЩЕНИЕ: Спасибо за покупку
        ShowTemporaryMessage("СПАСИБО ЗА ПОКУПКУ!", 2000);

        // Уведомляем станцию, если товар закончился
        if (tray.IsEmpty)
        {
            NotifyStation(tray);
        }

        _currentAmount = 0;
        _selectedProduct = null;
    }

    public void ReturnMoney()
    {
        ResetIdleTimer();
        ResetDisplayTimer();

        if (_currentAmount > 0)
        {
            // СООБЩЕНИЕ: Возврат денег
            ShowTemporaryMessage($"ВОЗВРАТ: {_currentAmount}₽", 2000);

            _currentAmount = 0;
            _selectedProduct = null;
        }
        else
        {
            ResetDisplay();
        }
    }

    private void ShowTemporaryMessage(string message, int durationMs)
    {
        _showingTemporaryMessage = true;
        _temporaryMessage = message;

        // Останавливаем предыдущий таймер сообщения
        _messageTimer?.Stop();

        // Создаем новый таймер для этого сообщения
        _messageTimer.Interval = durationMs;
        _messageTimer.Tick -= ClearTemporaryMessage;
        _messageTimer.Tick += ClearTemporaryMessage;
        _messageTimer.Start();

        DisplayUpdated?.Invoke();
    }

    private void ClearTemporaryMessage(object sender = null, EventArgs e = null)
    {
        _showingTemporaryMessage = false;
        _messageTimer?.Stop();

        // Возвращаем обычное отображение
        _display.UpdateDisplay(_currentAmount, _selectedProduct);
        DisplayUpdated?.Invoke();
    }

    private void UpdateDisplay()
    {
        if (!_showingTemporaryMessage)
        {
            _display.UpdateDisplay(_currentAmount, _selectedProduct);
            DisplayUpdated?.Invoke();
        }
    }

    private void ResetDisplay()
    {
        _selectedProduct = null;
        UpdateDisplay();
    }

    private void ResetIdleTimer()
    {
        if (_idleTimer != null)
        {
            _idleTimer.Stop();
            _idleTimer.Start();
        }
    }

    private void ResetDisplayTimer()
    {
        if (_displayResetTimer != null)
        {
            _displayResetTimer.Stop();
            _displayResetTimer.Start();
        }
    }

    private void NotifyStation(Tray tray)
    {
        var message = $"Товар '{tray.Product.Name}' в лотке {tray.Number} закончился";

        try
        {
            File.AppendAllText("station_notifications.log",
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
        }
        catch (Exception)
        {
            // Игнорируем ошибки записи в лог
        }

        StationNotification?.Invoke(message);
    }

    public void RequestAdmin()
    {
        AdminRequested?.Invoke();
    }

    public void ReloadProducts()
    {
        var products = _context.Products.ToList();
        foreach (var tray in _trays)
        {
            var product = products.FirstOrDefault(p => p.SlotNumber == tray.Number);
            if (product != null)
            {
                tray.Product = product;
                TrayStatusChanged?.Invoke(tray.Number - 1, tray.IsEmpty);
            }
        }
        UpdateDisplay();
    }

    public decimal CollectMoney()
    {
        return _moneyBox.Collect();
    }

    public bool UnlockAdmin(string code)
    {
        return _lock.Unlock(code);
    }

    public void Dispose()
    {
        _idleTimer?.Dispose();
        _displayResetTimer?.Dispose();
        _messageTimer?.Dispose();
    }
}