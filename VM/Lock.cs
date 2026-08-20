public class Lock
{
    public bool IsLocked { get; private set; } = true;
    private string _secretCode = "admin123";

    public bool Unlock(string code)
    {
        if (code == _secretCode)
        {
            IsLocked = false;
            return true;
        }
        return false;
    }

    public void LockIt()
    {
        IsLocked = true;
    }
}