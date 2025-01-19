using TMPro;  // Ensure TMP is included
using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    [Header("Temperature Settings")]
    public float currentTemperature;

    [Header("Temperature UI")]
    [SerializeField] private TMP_Text temperatureText;  // Use TMP_Text instead of UnityEngine.UI.Text

    private TimeManager timeManager;

    private void Awake()
    {
        // Find the TimeManager in the scene if it's not on the same GameObject
        timeManager = FindObjectOfType<TimeManager>();

        // Debug logs to check if the components are assigned
        if (timeManager == null)
        {
            Debug.LogError("TimeManager is not assigned!");
        }

        if (temperatureText == null)
        {
            Debug.LogError("Temperature Text is not assigned!");
        }
    }

    public void Update()
    {
        if (timeManager != null)
        {
            UpdateTemperature();
        }
        UpdateTemperatureUI();
    }

    private void UpdateTemperature()
    {
        int hour = timeManager.Hours;

        if (hour >= 0 && hour < 6) // Night
        {
            currentTemperature = Mathf.Lerp(10f, 20f, Mathf.InverseLerp(0f, 6f, hour));
        }
        else if (hour >= 6 && hour < 8) // Sunrise
        {
            currentTemperature = Mathf.Lerp(20f, 30f, Mathf.InverseLerp(6f, 8f, hour));
        }
        else if (hour >= 8 && hour < 18) // Day
        {
            currentTemperature = Mathf.Lerp(30f, 40f, Mathf.InverseLerp(8f, 18f, hour));
        }
        else if (hour >= 18 && hour < 22) // Sunset
        {
            currentTemperature = Mathf.Lerp(40f, 25f, Mathf.InverseLerp(18f, 22f, hour));
        }
        else // Night
        {
            currentTemperature = Mathf.Lerp(25f, 10f, Mathf.InverseLerp(22f, 24f, hour));
        }
    }

    private void UpdateTemperatureUI()
    {
        if (temperatureText != null)
        {
            temperatureText.text = "Temperature: " + Mathf.RoundToInt(currentTemperature) + "°C";
        }
    }
}
