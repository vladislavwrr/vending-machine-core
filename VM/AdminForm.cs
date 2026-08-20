using System;
using System.Linq;
using System.Windows.Forms;

public class AdminForm : Form
{
    private readonly VendingMachine _vendingMachine;
    private readonly ProductContext _context;
    private DataGridView dataGridView1;
    private Button btnReloadProducts;
    private Button btnCollectMoney;
    private Button btnSaveChanges;
    private Button btnRefresh;

    public AdminForm(VendingMachine vendingMachine)
    {
        _vendingMachine = vendingMachine;
        _context = new ProductContext();
        InitializeComponent();
        LoadProducts();
    }

    private void InitializeComponent()
    {
        this.dataGridView1 = new DataGridView();
        this.btnReloadProducts = new Button();
        this.btnCollectMoney = new Button();
        this.btnSaveChanges = new Button();
        this.btnRefresh = new Button();
        this.SuspendLayout();

        // DataGridView
        this.dataGridView1.Location = new System.Drawing.Point(12, 12);
        this.dataGridView1.Size = new System.Drawing.Size(600, 200);
        this.dataGridView1.AutoGenerateColumns = true;

        // Buttons
        this.btnRefresh.Location = new System.Drawing.Point(12, 220);
        this.btnRefresh.Size = new System.Drawing.Size(100, 30);
        this.btnRefresh.Text = "Обновить";
        this.btnRefresh.Click += btnRefresh_Click;

        this.btnSaveChanges.Location = new System.Drawing.Point(120, 220);
        this.btnSaveChanges.Size = new System.Drawing.Size(100, 30);
        this.btnSaveChanges.Text = "Сохранить";
        this.btnSaveChanges.Click += btnSaveChanges_Click;

        this.btnReloadProducts.Location = new System.Drawing.Point(230, 220);
        this.btnReloadProducts.Size = new System.Drawing.Size(100, 30);
        this.btnReloadProducts.Text = "Перезагрузить";
        this.btnReloadProducts.Click += btnReloadProducts_Click;

        this.btnCollectMoney.Location = new System.Drawing.Point(340, 220);
        this.btnCollectMoney.Size = new System.Drawing.Size(100, 30);
        this.btnCollectMoney.Text = "Выручка";
        this.btnCollectMoney.Click += btnCollectMoney_Click;

        // Form settings
        this.ClientSize = new System.Drawing.Size(624, 261);
        this.Text = "Администрирование";
        this.Controls.Add(dataGridView1);
        this.Controls.Add(btnRefresh);
        this.Controls.Add(btnSaveChanges);
        this.Controls.Add(btnReloadProducts);
        this.Controls.Add(btnCollectMoney);

        this.ResumeLayout(false);
    }

    private void LoadProducts()
    {
        var products = _context.Products.ToList();
        dataGridView1.DataSource = null;
        dataGridView1.DataSource = products;
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadProducts();
    }

    private void btnSaveChanges_Click(object sender, EventArgs e)
    {
        try
        {
            var products = _context.Products;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var productId = Convert.ToInt32(row.Cells["Id"].Value);
                var product = products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    product.Name = row.Cells["Name"].Value?.ToString() ?? "";
                    product.Price = Convert.ToDecimal(row.Cells["Price"].Value);
                    product.Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                }
            }

            _context.SaveChanges();
            MessageBox.Show("Изменения сохранены в базе данных!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения: {ex.Message}");
        }
    }

    private void btnReloadProducts_Click(object sender, EventArgs e)
    {
        _vendingMachine?.ReloadProducts();
        MessageBox.Show("Товары перезагружены в автомат!");
    }

    private void btnCollectMoney_Click(object sender, EventArgs e)
    {
        var collected = _vendingMachine?.CollectMoney() ?? 0;
        MessageBox.Show($"Выручка изъята: {collected}₽\nЯщик с деньгами закрыт.");
    }
}