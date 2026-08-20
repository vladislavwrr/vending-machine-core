using System.Drawing;
using System.Drawing.Drawing2D;

public class Lamp
{
    public Color Color { get; private set; } = Color.Gray;
    public bool IsLit { get; private set; }

    public void SetColor(Color color)
    {
        Color = color;
        IsLit = true;
    }

    public void TurnOff()
    {
        IsLit = false;
        Color = Color.Gray;
    }
}