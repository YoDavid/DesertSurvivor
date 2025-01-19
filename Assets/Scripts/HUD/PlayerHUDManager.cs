using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    public PlayerStatsManager playerStatsManager;

    // UI elements (only reference the bars)
    public Image healthBar;   // Filler Image (health bar)
    public Image thirstBar;   // Filler Image (thirst bar)
    public Image hungerBar;   // Filler Image (hunger bar)

    void Start()
    {
        // Ensure PlayerStatsManager is assigned in the Inspector
        if (playerStatsManager == null)
        {
            Debug.LogError("PlayerStatsManager not assigned in PlayerHUDManager.");
        }
    }

    void Update()
    {
        // Update the HUD elements with the current stats
        UpdateHealthHUD();
        UpdateThirstHUD();
        UpdateHungerHUD();
    }

    private void UpdateHealthHUD()
    {
        // Update health bar
        healthBar.fillAmount = playerStatsManager.CurrentHealth / playerStatsManager.MaxHealth;
    }

    private void UpdateThirstHUD()
    {
        // Update thirst bar
        thirstBar.fillAmount = playerStatsManager.CurrentThirst / playerStatsManager.MaxThirst;
    }

    private void UpdateHungerHUD()
    {
        // Update hunger bar
        hungerBar.fillAmount = playerStatsManager.CurrentHunger / playerStatsManager.MaxHunger;
    }
}
