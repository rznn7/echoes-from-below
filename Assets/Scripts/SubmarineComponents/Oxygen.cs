public class Oxygen
{
    public int MaxAmount = 100;
    public int CurrentAmount;
    
    public Oxygen()
    {
        CurrentAmount = MaxAmount;
    }
    
    public void AddOxygen(int amount)
    {
        CurrentAmount += amount;
        if (CurrentAmount > MaxAmount)
        {
            CurrentAmount = MaxAmount;
        }
    }
    
    public void SubtractOxygen(int amount)
    {
        CurrentAmount -= amount;
        if (CurrentAmount < 0)
        {
            CurrentAmount = 0;
        }
    }
}