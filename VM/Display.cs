public class Display
{
    public string Text { get; private set; } = "ДОБРО ПОЖАЛОВАТЬ";

    public void UpdateDisplay(decimal currentAmount, Product selectedProduct)
    {
        string displayText = $"ВНЕСЕНО: {currentAmount}₽\n\n";

        if (selectedProduct != null)
        {
            displayText += $"ТОВАР: {selectedProduct.Name}\n";
            displayText += $"ЦЕНА: {selectedProduct.Price}₽\n";

            if (currentAmount < selectedProduct.Price)
            {
                decimal needed = selectedProduct.Price - currentAmount;
                displayText += $"НУЖНО: {needed}₽";
            }
            else
            {
                displayText += "ГОТОВО К ПОКУПКЕ";
            }
        }
        else
        {
            displayText += "ВЫБЕРИТЕ ТОВАР";
        }

        Text = displayText;
    }

    public void ShowMessage(string message)
    {
        Text = message;
    }

    public void Reset()
    {
        Text = "ДОБРО ПОЖАЛОВАТЬ";
    }
}