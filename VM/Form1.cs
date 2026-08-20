using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private readonly VendingMachine _vendingMachine;
    private readonly List<Button> _slotButtons;
    private readonly List<Label> _lampLabels;

    public Form1()
    {
        InitializeComponent();

        // Загрузка изображения
        try
        {
            this.pictureBox1.Image = Image.FromFile("C:\\Users\\dinoz\\OneDrive\\Рабочий стол\\Документы\\ПНИПУ\\3 курс\\Проектирование архитектуры программных систем\\Задание 1\\Design\\5PixelVendingMachine.png");
        }
        catch
        {
            this.pictureBox1.Image = CreatePlaceholderImage(450, 450);
        }

        _vendingMachine = new VendingMachine();
        _slotButtons = new List<Button>();
        _lampLabels = new List<Label>();

        SetupSlotButtons();
        SetupLamps();

        BringLampsToFront();

        // Подписка на события VendingMachine
        _vendingMachine.DisplayUpdated += UpdateDisplayFromVendingMachine;
        _vendingMachine.StationNotification += OnStationNotification;
        _vendingMachine.AdminRequested += OnAdminRequested;
        _vendingMachine.TrayStatusChanged += UpdateLampColor;

        // Инициализация дисплея
        UpdateDisplayFromVendingMachine();
    }

    // Обновление цвета лампочки
    private void UpdateLampColor(int slotIndex, bool isEmpty)
    {
        if (slotIndex >= 0 && slotIndex < _lampLabels.Count)
        {
            if (_lampLabels[slotIndex].InvokeRequired)
            {
                _lampLabels[slotIndex].Invoke(new Action(() =>
                {
                    _lampLabels[slotIndex].BackColor = isEmpty ? Color.Red : Color.LimeGreen;
                }));
            }
            else
            {
                _lampLabels[slotIndex].BackColor = isEmpty ? Color.Red : Color.LimeGreen;
            }
        }
    }

    private void BringLampsToFront()
    {
        foreach (Control control in panelImageContainer.Controls)
        {
            if (control is Label)
            {
                control.BringToFront();
            }
        }
    }

    private Bitmap CreatePlaceholderImage(int width, int height)
    {
        Bitmap bmp = new Bitmap(width, height);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.LightGray);
            using (Font font = new Font("Arial", 16))
            using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString("Изображение\nторгового\nавтомата", font, Brushes.Black, new Rectangle(0, 0, width, height), sf);
            }
            g.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);
        }
        return bmp;
    }

    private void SetupSlotButtons()
    {
        _slotButtons.AddRange(new[] {
            btnSlot1, btnSlot2, btnSlot3, btnSlot4,
            btnSlot5, btnSlot6, btnSlot7, btnSlot8
        });

        // Инициализация текста кнопок
        for (int i = 0; i < _slotButtons.Count; i++)
        {
            _slotButtons[i].Text = $"{i + 1}";
            _slotButtons[i].Tag = i + 1;
        }
    }

    private void SetupLamps()
    {
        _lampLabels.AddRange(new[] {
            lamp1, lamp2, lamp3, lamp4,
            lamp5, lamp6, lamp7, lamp8
        });

        // Инициализация цветов лампочек (все зеленые по умолчанию)
        foreach (var lamp in _lampLabels)
        {
            lamp.BackColor = Color.LimeGreen;
        }
    }

    private void UpdateDisplayFromVendingMachine()
    {
        if (lblDisplay.InvokeRequired)
        {
            lblDisplay.Invoke(new Action(() => lblDisplay.Text = _vendingMachine.DisplayText));
        }
        else
        {
            lblDisplay.Text = _vendingMachine.DisplayText;
        }
    }

    private void btnMoney_Click(object sender, EventArgs e)
    {
        if (sender is Button button && decimal.TryParse(button.Tag?.ToString(), out decimal amount))
        {
            _vendingMachine.InsertMoney(amount);
        }
    }

    private void btnSlot_Click(object sender, EventArgs e)
    {
        if (sender is Button button && int.TryParse(button.Tag?.ToString(), out int slotNumber))
        {
            _vendingMachine.SelectSlot(slotNumber);
        }
    }

    private void btnPurchase_Click(object sender, EventArgs e)
    {
        _vendingMachine.Purchase();
    }

    private void btnReturn_Click(object sender, EventArgs e)
    {
        _vendingMachine.ReturnMoney();
    }

    private void OnStationNotification(string message)
    {
        if (lblStationNotifications.InvokeRequired)
        {
            lblStationNotifications.Invoke(new Action(() =>
                lblStationNotifications.Text += $"{DateTime.Now:HH:mm:ss} - {message}\n"));
        }
        else
        {
            lblStationNotifications.Text += $"{DateTime.Now:HH:mm:ss} - {message}\n";
        }
    }

    private void OnAdminRequested()
    {
        var adminForm = new AdminForm(_vendingMachine);
        adminForm.ShowDialog();
    }

    private void SecretAdminButton_Click(object sender, EventArgs e)
    {
        _vendingMachine.RequestAdmin();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _vendingMachine?.Dispose();
        base.OnFormClosing(e);
    }
}