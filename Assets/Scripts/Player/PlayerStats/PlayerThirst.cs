public class PlayerThirst : PlayerStats
{
    public PlayerThirst() : base(100f) // Initialize with max thirst value
    { }

    public void Drink(float amount)
    {
        UpdateStat(amount); // Increase thirst when drinking
    }

    public void DrainThirst(float amount)
    {
        UpdateStat(-amount); // Decrease thirst over time
    }
}
