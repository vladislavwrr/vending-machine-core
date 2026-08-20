using System.Drawing;

public class Tray
{
    public int Number { get; set; }
    public Product Product { get; set; }
    public bool IsEmpty => Product?.IsEmpty ?? true;

    public Tray(int number)
    {
        Number = number;
    }
}