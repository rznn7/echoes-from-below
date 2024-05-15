public class Power
{
    public int MaxAmount = 100;
    public int CurrentAmount;
    
    public Power()
    {
        CurrentAmount = MaxAmount;
    }
    
    public void AddPower(int amount)
    {
        CurrentAmount += amount;
        if (CurrentAmount > MaxAmount)
        {
            CurrentAmount = MaxAmount;
        }
    }
    
    public void SubtractPower(int amount)
    {
        CurrentAmount -= amount;
        if (CurrentAmount < 0)
        {
            CurrentAmount = 0;
        }
    }
}
