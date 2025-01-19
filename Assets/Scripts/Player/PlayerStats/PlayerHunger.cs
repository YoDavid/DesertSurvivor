public class PlayerHunger : PlayerStats
{
    public PlayerHunger() : base(100f) // Initialize with max hunger value
    { }

    public void Eat(float amount)
    {
        UpdateStat(amount); // Increase hunger when eating
    }

    public void DrainHunger(float amount)
    {
        UpdateStat(-amount); // Decrease hunger over time
    }
}
