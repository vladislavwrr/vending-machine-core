using System.Collections.Generic;
using System.Linq;

public class ProductContext
{
    private static List<Product> _products;

    public ProductContext()
    {
        // Инициализируем тестовыми данными только один раз
        if (_products == null)
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Кола", Price = 100, SlotNumber = 1, Quantity = 5 },
                new Product { Id = 2, Name = "Фанта", Price = 100, SlotNumber = 2, Quantity = 5 },
                new Product { Id = 3, Name = "Спрайт", Price = 100, SlotNumber = 3, Quantity = 5 },
                new Product { Id = 4, Name = "Вода", Price = 50, SlotNumber = 4, Quantity = 5 },
                new Product { Id = 5, Name = "Сок", Price = 120, SlotNumber = 5, Quantity = 5 },
                new Product { Id = 6, Name = "Чай", Price = 80, SlotNumber = 6, Quantity = 5 },
                new Product { Id = 7, Name = "Кофе", Price = 150, SlotNumber = 7, Quantity = 5 },
                new Product { Id = 8, Name = "Энергетик", Price = 200, SlotNumber = 8, Quantity = 5 }
            };
        }
    }

    public List<Product> Products => _products;

    public void SaveChanges()
    {
        // В нашем случае изменения сохраняются в статическом списке
        // и доступны для всех экземпляров ProductContext
    }
}