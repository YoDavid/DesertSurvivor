using UnityEngine;

public class PlayerStats
{
    public float currentValue;
    public float maxValue;

    public PlayerStats(float maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue;
    }

    public virtual void UpdateStat(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
    }

    public virtual void ResetStat()
    {
        currentValue = maxValue;
    }
}
