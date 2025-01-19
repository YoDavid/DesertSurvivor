using TMPro;  // Ensure TMP is included
using UnityEngine;

public class TemperatureHUDManager : MonoBehaviour
{
    [Header("Temperature HUD")]
    [SerializeField] private TMP_Text temperatureText;

    private TemperatureManager temperatureManager;

    private void Awake()
    {
        // Find the TemperatureManager in the scene
        temperatureManager = FindObjectOfType<TemperatureManager>();

        if (temperatureManager == null)
        {
            Debug.LogError("TemperatureManager is not assigned!");
        }

        if (temperatureText == null)
        {
            Debug.LogError("Temperature Text is not assigned!");
        }
    }

    private void Update()
    {
        // Ensure temperatureManager is valid
        if (temperatureManager != null)
        {
            // Update UI with current temperature
            temperatureText.text = Mathf.RoundToInt(temperatureManager.currentTemperature) + "°C";
        }
    }
}
