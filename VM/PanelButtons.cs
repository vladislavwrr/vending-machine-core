using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class PanelButtons
{
    // События для кнопок
    public event EventHandler<MoneyInsertedEventArgs> MoneyInserted;
    public event EventHandler<SlotSelectedEventArgs> SlotSelected;
    public event EventHandler PurchaseClicked;
    public event EventHandler ReturnClicked;
    public event EventHandler AdminClicked;

    // Коллекции кнопок
    public Dictionary<decimal, Button> MoneyButtons { get; } = new Dictionary<decimal, Button>();
    public Dictionary<int, Button> SlotButtons { get; } = new Dictionary<int, Button>();
    public Button PurchaseButton { get; private set; }
    public Button ReturnButton { get; private set; }
    public Button AdminButton { get; private set; }

    public void Initialize()
    {
        // Инициализация денежных кнопок
        MoneyButtons[10m] = CreateMoneyButton(10, "10₽");
        MoneyButtons[50m] = CreateMoneyButton(50, "50₽");
        MoneyButtons[100m] = CreateMoneyButton(100, "100₽");

        // Инициализация кнопок слотов
        for (int i = 1; i <= 8; i++)
        {
            SlotButtons[i] = CreateSlotButton(i, i.ToString());
        }

        // Кнопки действий
        PurchaseButton = new Button
        {
            Text = "КУПИТЬ",
            BackColor = System.Drawing.Color.LimeGreen,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Size = new System.Drawing.Size(180, 50)
        };

        ReturnButton = new Button
        {
            Text = "ВОЗВРАТ",
            BackColor = System.Drawing.Color.OrangeRed,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Size = new System.Drawing.Size(180, 50)
        };

        AdminButton = new Button
        {
            Text = "⚙",
            BackColor = System.Drawing.Color.Gold,
            Font = new System.Drawing.Font("Arial", 16)
        };

        // Подписка на события
        foreach (var button in MoneyButtons.Values)
        {
            button.Click += (s, e) => MoneyInserted?.Invoke(s, new MoneyInsertedEventArgs(decimal.Parse(button.Text.Replace("₽", ""))));
        }

        foreach (var button in SlotButtons.Values)
        {
            button.Click += (s, e) => SlotSelected?.Invoke(s, new SlotSelectedEventArgs(int.Parse(button.Text)));
        }

        PurchaseButton.Click += (s, e) => PurchaseClicked?.Invoke(s, e);
        ReturnButton.Click += (s, e) => ReturnClicked?.Invoke(s, e);
        AdminButton.Click += (s, e) => AdminClicked?.Invoke(s, e);
    }

    private Button CreateMoneyButton(decimal amount, string text)
    {
        return new Button
        {
            Text = text,
            Tag = amount,
            Size = new System.Drawing.Size(80, 40),
            BackColor = System.Drawing.Color.SteelBlue,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold)
        };
    }

    private Button CreateSlotButton(int slotNumber, string text)
    {
        return new Button
        {
            Text = text,
            Tag = slotNumber,
            Size = new System.Drawing.Size(80, 80),
            BackColor = System.Drawing.Color.Black,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold)
        };
    }
}

// Классы аргументов событий
public class MoneyInsertedEventArgs : EventArgs
{
    public decimal Amount { get; }

    public MoneyInsertedEventArgs(decimal amount)
    {
        Amount = amount;
    }
}

public class SlotSelectedEventArgs : EventArgs
{
    public int SlotNumber { get; }

    public SlotSelectedEventArgs(int slotNumber)
    {
        SlotNumber = slotNumber;
    }
}