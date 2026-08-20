using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    // Контролы
    private Panel panelImageContainer;
    private PictureBox pictureBox1;
    private Label lblDisplay;
    private Label lblMessage;
    private Label lblStationNotifications;
    private Button btnSlot1;
    private Button btnSlot2;
    private Button btnSlot3;
    private Button btnSlot4;
    private Button btnSlot5;
    private Button btnSlot6;
    private Button btnSlot7;
    private Button btnSlot8;
    private Button btnMoney10;
    private Button btnMoney50;
    private Button btnMoney100;
    private Button btnPurchase;
    private Button btnReturn;
    private Button btnSecretAdmin;
    private Label lamp1;
    private Label lamp2;
    private Label lamp3;
    private Label lamp4;
    private Label lamp5;
    private Label lamp6;
    private Label lamp7;
    private Label lamp8;
    private Panel panelControlSection;
    private Panel panelMoneySection;
    private Panel panelSlotsSection;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.panelImageContainer = new Panel();
        this.pictureBox1 = new PictureBox();
        this.lblDisplay = new Label();
        this.lblMessage = new Label();
        this.lblStationNotifications = new Label();
        this.btnSlot1 = new Button();
        this.btnSlot2 = new Button();
        this.btnSlot3 = new Button();
        this.btnSlot4 = new Button();
        this.btnSlot5 = new Button();
        this.btnSlot6 = new Button();
        this.btnSlot7 = new Button();
        this.btnSlot8 = new Button();
        this.btnMoney10 = new Button();
        this.btnMoney50 = new Button();
        this.btnMoney100 = new Button();
        this.btnPurchase = new Button();
        this.btnReturn = new Button();
        this.btnSecretAdmin = new Button();
        this.lamp1 = new Label();
        this.lamp2 = new Label();
        this.lamp3 = new Label();
        this.lamp4 = new Label();
        this.lamp5 = new Label();
        this.lamp6 = new Label();
        this.lamp7 = new Label();
        this.lamp8 = new Label();
        this.panelControlSection = new Panel();
        this.panelMoneySection = new Panel();
        this.panelSlotsSection = new Panel();

        // SuspendLayout
        this.SuspendLayout();

        // 
        // panelImageContainer
        // 
        this.panelImageContainer.Location = new Point(15, 15);
        this.panelImageContainer.Size = new Size(450, 450);
        this.panelImageContainer.BorderStyle = BorderStyle.FixedSingle;
        this.panelImageContainer.BackColor = Color.White;
        this.panelImageContainer.Padding = new Padding(2);

        // 
        // Лампочки
        // 
        this.lamp1 = CreateLampLabel(50, 0);
        this.lamp2 = CreateLampLabel(160, 0);
        this.lamp3 = CreateLampLabel(260, 0);
        this.lamp4 = CreateLampLabel(370, 0);
        this.lamp5 = CreateLampLabel(70, 240);
        this.lamp6 = CreateLampLabel(175, 240);
        this.lamp7 = CreateLampLabel(275, 240);
        this.lamp8 = CreateLampLabel(370, 240);

        // 
        // pictureBox1
        // 
        this.pictureBox1.Location = new Point(0, 0);
        this.pictureBox1.Size = new Size(450, 450);
        this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        this.pictureBox1.BackColor = Color.LightGray;

        // 
        // panelControlSection
        // 
        this.panelControlSection.Location = new Point(480, 15);
        this.panelControlSection.Size = new Size(400, 450);
        this.panelControlSection.BorderStyle = BorderStyle.FixedSingle;
        this.panelControlSection.BackColor = Color.LightGray;
        this.panelControlSection.Padding = new Padding(8);

        // 
        // lblDisplay
        // 
        this.lblDisplay.Location = new Point(10, 10);
        this.lblDisplay.Size = new Size(380, 120);
        this.lblDisplay.BorderStyle = BorderStyle.FixedSingle;
        this.lblDisplay.Font = new Font("Consolas", 12, FontStyle.Bold);
        this.lblDisplay.TextAlign = ContentAlignment.MiddleCenter;
        this.lblDisplay.Text = "ВНЕСЕНО: 0₽\n\nВЫБЕРИТЕ ТОВАР";
        this.lblDisplay.BackColor = Color.Black;
        this.lblDisplay.ForeColor = Color.Lime;

        // 
        // panelMoneySection
        // 
        this.panelMoneySection.Location = new Point(10, 140);
        this.panelMoneySection.Size = new Size(380, 60);
        this.panelMoneySection.BackColor = Color.Transparent;

        // 
        // Кнопки денег
        // 
        this.btnMoney10 = CreateMoneyButton(40, 10, 10, "10");
        this.btnMoney50 = CreateMoneyButton(145, 10, 50, "50");
        this.btnMoney100 = CreateMoneyButton(250, 10, 100, "100");

        this.panelMoneySection.Controls.Add(btnMoney10);
        this.panelMoneySection.Controls.Add(btnMoney50);
        this.panelMoneySection.Controls.Add(btnMoney100);

        // 
        // Кнопки покупки и возврата
        // 
        this.btnPurchase = CreateActionButton(15, 210, "КУПИТЬ", Color.LimeGreen);
        this.btnReturn = CreateActionButton(205, 210, "ВОЗВРАТ", Color.OrangeRed);

        // 
        // panelSlotsSection
        // 
        this.panelSlotsSection.Location = new Point(10, 270);
        this.panelSlotsSection.Size = new Size(380, 170);
        this.panelSlotsSection.BackColor = Color.Transparent;

        // 
        // Кнопки лотков
        // 
        this.btnSlot1 = CreateSlotButton(15, 10, 1, "1");
        this.btnSlot2 = CreateSlotButton(105, 10, 2, "2");
        this.btnSlot3 = CreateSlotButton(195, 10, 3, "3");
        this.btnSlot4 = CreateSlotButton(285, 10, 4, "4");
        this.btnSlot5 = CreateSlotButton(15, 90, 5, "5");
        this.btnSlot6 = CreateSlotButton(105, 90, 6, "6");
        this.btnSlot7 = CreateSlotButton(195, 90, 7, "7");
        this.btnSlot8 = CreateSlotButton(285, 90, 8, "8");

        this.panelSlotsSection.Controls.Add(btnSlot1);
        this.panelSlotsSection.Controls.Add(btnSlot2);
        this.panelSlotsSection.Controls.Add(btnSlot3);
        this.panelSlotsSection.Controls.Add(btnSlot4);
        this.panelSlotsSection.Controls.Add(btnSlot5);
        this.panelSlotsSection.Controls.Add(btnSlot6);
        this.panelSlotsSection.Controls.Add(btnSlot7);
        this.panelSlotsSection.Controls.Add(btnSlot8);

        // 
        // Уведомления станции
        // 
        this.lblStationNotifications.Location = new Point(15, 475);
        this.lblStationNotifications.Size = new Size(450, 100);
        this.lblStationNotifications.BorderStyle = BorderStyle.FixedSingle;
        this.lblStationNotifications.Font = new Font("Arial", 8);
        this.lblStationNotifications.Text = "УВЕДОМЛЕНИЯ:\n";
        this.lblStationNotifications.BackColor = Color.LightGray;

        // 
        // lblMessage
        // 
        this.lblMessage.Location = new Point(480, 150);
        this.lblMessage.Size = new Size(400, 40);
        this.lblMessage.Font = new Font("Arial", 10);
        this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
        this.lblMessage.ForeColor = Color.Red;
        this.lblMessage.Visible = false;

        // 
        // Кнопка админа
        // 
        this.btnSecretAdmin = new Button();
        this.btnSecretAdmin.Location = new Point(830, 530);
        this.btnSecretAdmin.Size = new Size(50, 50);
        this.btnSecretAdmin.Text = "⚙";
        this.btnSecretAdmin.BackColor = Color.Gold;
        this.btnSecretAdmin.FlatStyle = FlatStyle.Flat;
        this.btnSecretAdmin.Font = new Font("Arial", 16);
        this.btnSecretAdmin.Click += SecretAdminButton_Click;

        // Добавляем все элементы
        this.panelImageContainer.Controls.Add(pictureBox1);
        this.panelImageContainer.Controls.Add(lamp1);
        this.panelImageContainer.Controls.Add(lamp2);
        this.panelImageContainer.Controls.Add(lamp3);
        this.panelImageContainer.Controls.Add(lamp4);
        this.panelImageContainer.Controls.Add(lamp5);
        this.panelImageContainer.Controls.Add(lamp6);
        this.panelImageContainer.Controls.Add(lamp7);
        this.panelImageContainer.Controls.Add(lamp8);

        this.panelControlSection.Controls.Add(lblDisplay);
        this.panelControlSection.Controls.Add(panelMoneySection);
        this.panelControlSection.Controls.Add(btnPurchase);
        this.panelControlSection.Controls.Add(btnReturn);
        this.panelControlSection.Controls.Add(panelSlotsSection);

        this.Controls.Add(panelImageContainer);
        this.Controls.Add(panelControlSection);
        this.Controls.Add(lblStationNotifications);
        this.Controls.Add(lblMessage);
        this.Controls.Add(btnSecretAdmin);

        // 
        // Form
        // 
        this.ClientSize = new Size(900, 590);
        this.Text = "Торговый автомат";
        this.BackColor = Color.LightGray;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        this.ResumeLayout(false);
    }

    private Button CreateSlotButton(int x, int y, int slotNumber, string text)
    {
        var button = new Button();
        button.Location = new Point(x, y);
        button.Size = new Size(80, 80);
        button.Text = text;
        button.Tag = slotNumber;
        button.BackColor = Color.Black;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Arial", 18, FontStyle.Bold);
        button.Click += btnSlot_Click;
        return button;
    }

    private Button CreateMoneyButton(int x, int y, decimal amount, string text)
    {
        var button = new Button();
        button.Location = new Point(x, y);
        button.Size = new Size(80, 40);
        button.Text = text + "₽";
        button.Tag = amount;
        button.BackColor = Color.SteelBlue;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Arial", 11, FontStyle.Bold);
        button.Click += btnMoney_Click;
        return button;
    }

    private Button CreateActionButton(int x, int y, string text, Color color)
    {
        var button = new Button();
        button.Location = new Point(x, y);
        button.Size = new Size(180, 50);
        button.Text = text;
        button.BackColor = color;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Arial", 12, FontStyle.Bold);

        if (text == "КУПИТЬ")
            button.Click += btnPurchase_Click;
        else
            button.Click += btnReturn_Click;

        return button;
    }

    private Label CreateLampLabel(int x, int y)
    {
        var label = new Label();
        label.Location = new Point(x, y);
        label.Size = new Size(20, 20);
        label.BackColor = Color.Gray;
        label.BorderStyle = BorderStyle.FixedSingle;

        // Делаем лампочку круглой
        using (var path = new GraphicsPath())
        {
            path.AddEllipse(0, 0, label.Width, label.Height);
            label.Region = new Region(path);
        }

        return label;
    }
}