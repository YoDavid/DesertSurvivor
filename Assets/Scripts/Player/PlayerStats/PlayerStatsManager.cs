using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    // Player's current and max stats
    [Header("Player Stats")]
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float currentThirst = 100f;
    [SerializeField] private float maxThirst = 100f;

    [SerializeField] private float currentHunger = 100f;
    [SerializeField] private float maxHunger = 100f;

    // Public properties to access stats
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float CurrentThirst
    {
        get { return currentThirst; }
        set { currentThirst = Mathf.Clamp(value, 0, maxThirst); }
    }

    public float MaxThirst
    {
        get { return maxThirst; }
        set { maxThirst = value; }
    }

    public float CurrentHunger
    {
        get { return currentHunger; }
        set { currentHunger = Mathf.Clamp(value, 0, maxHunger); }
    }

    public float MaxHunger
    {
        get { return maxHunger; }
        set { maxHunger = value; }
    }

    // Reference to health, thirst, and hunger bars
    [Header("UI Bars")]
    public Image healthBar;
    public Image thirstBar;
    public Image hungerBar;

    // Update method called every frame
    void Update()
    {
        // Health reduction based on low thirst or hunger
        if (currentThirst <= 20f)
        {
            currentHealth -= Time.deltaTime * 0.5f;
        }

        if (currentHunger <= 20f)
        {
            currentHealth -= Time.deltaTime * 0.5f;
        }

        // Check for instant death if either thirst or hunger reaches 0
        if (currentThirst <= 0f || currentHunger <= 0f)
        {
            currentHealth = 0f; // Player dies immediately
            Die(); // Trigger death
        }

        // Update the UI bars
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;

        if (thirstBar != null)
            thirstBar.fillAmount = currentThirst / maxThirst;

        if (hungerBar != null)
            hungerBar.fillAmount = currentHunger / maxHunger;
    }

    // Drink water: Fill the thirst and slightly heal
    public void DrinkWater(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        currentHealth = Mathf.Clamp(currentHealth + amount * 0.2f, 0, maxHealth);  // Healing effect from drinking water
        UpdateUI(); // Update the UI after drinking
    }

    // Eat food: Fill hunger and heal the player
    public void EatFood(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        currentHealth = Mathf.Clamp(currentHealth + amount * 0.5f, 0, maxHealth);  // Healing effect from eating
        UpdateUI(); // Update the UI after eating
    }

    // Method to trigger player death
    private void Die()
    {
        Debug.Log("Player has died.");
        // Add any additional death logic here
    }
}
