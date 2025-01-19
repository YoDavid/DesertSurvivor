public class PlayerHealth : PlayerStats
{
    public PlayerHealth() : base(100f) // Initialize with max health value
    { }

    public void TakeDamage(float damage)
    {
        UpdateStat(-damage); // Decrease health when taking damage
    }

    public void Heal(float amount)
    {
        UpdateStat(amount); // Increase health when healing
    }
}
