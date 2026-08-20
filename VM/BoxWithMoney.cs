public class BoxWithMoney
{
    public decimal TotalAmount { get; private set; }

    public void Insert(decimal amount)
    {
        TotalAmount += amount;
    }

    public decimal Collect()
    {
        var collected = TotalAmount;
        TotalAmount = 0;
        return collected;
    }
}