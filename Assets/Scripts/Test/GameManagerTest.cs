using System.Collections;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    public static GameManagerTest Instance;

    // Day/Night Cycle Variables
    public float dayDuration = 60f;  // The length of one full day cycle (seconds)
    public float currentTimeOfDay = 0f; // Track the time of day (0 = start of the day, 1 = end of the day)
    public bool IsDaytime { get; private set; }

    // Skyboxes
    public Material daySkybox;
    public Material sunsetSkybox;
    public Material nightSkybox;
    public Material sunriseSkybox;
    private Material currentSkybox;
    public float transitionSpeed = 1f;  // Speed of the skybox transition

    // Light and Temperature Control
    public Light directionalLight;  // The sun's directional light
    public Color dayLightColor = new Color(1f, 1f, 1f, 1f);  // White light during the day
    public Color sunsetLightColor = new Color(1f, 0.5f, 0f, 1f);  // Orange-ish light during sunset
    public Color nightLightColor = new Color(0.1f, 0.1f, 0.2f, 1f);  // Dim, blue-ish light at night
    public Color sunriseLightColor = new Color(1f, 0.9f, 0.5f, 1f);  // Warm, yellowish light at sunrise
    public float dayTemperature = 30f;  // Temperature during the day (Celsius)
    public float nightTemperature = 5f;  // Temperature during the night (Celsius)

    // Debug Option
    public bool isManualTime = false; // Toggle between manual and automated time control
    [Range(0f, 1f)] public float manualTimeOfDay = 0f; // Manually set the time of day (0 = start of day, 1 = end of day)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    private void Start()
    {
        // Set initial skybox 
        RenderSettings.skybox = daySkybox;
        currentSkybox = daySkybox;
    }

    private void Update()
    {
        // If manual time is enabled, use the manual value instead of Time.deltaTime
        if (isManualTime)
        {
            currentTimeOfDay = Mathf.Clamp01(manualTimeOfDay); // Ensure the time of day is between 0 and 1
        }
        else
        {
            // Update the time of day in automated mode
            currentTimeOfDay += Time.deltaTime / dayDuration;
        }

        // Loop the time of day to stay within a 24-hour cycle
        if (currentTimeOfDay >= 1f)
        {
            currentTimeOfDay = 0f;
            SwitchDayNightCycle();
        }

        // Update the directional light (sun movement)
        UpdateLighting();

        // Optionally, trigger temperature-related changes (e.g., player needs to find shelter at night)
        UpdateTemperature();
    }

    private void SwitchDayNightCycle()
    {
        IsDaytime = !IsDaytime;

        // Smooth skybox transition
        if (currentTimeOfDay < 0.25f)  // Sunrise
        {
            StartCoroutine(SmoothSkyboxTransition(sunriseSkybox));
        }
        else if (currentTimeOfDay < 0.5f)  // Daytime
        {
            StartCoroutine(SmoothSkyboxTransition(daySkybox));
        }
        else if (currentTimeOfDay < 0.75f)  // Sunset
        {
            StartCoroutine(SmoothSkyboxTransition(sunsetSkybox));
        }
        else  // Nighttime
        {
            StartCoroutine(SmoothSkyboxTransition(nightSkybox));
        }
    }

    private IEnumerator SmoothSkyboxTransition(Material targetSkybox)
    {
        float timeElapsed = 0f;
        Material startSkybox = currentSkybox;

        while (timeElapsed < transitionSpeed)
        {
            RenderSettings.skybox.Lerp(startSkybox, targetSkybox, timeElapsed / transitionSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the target skybox is fully set at the end of the transition
        RenderSettings.skybox = targetSkybox;
        currentSkybox = targetSkybox;
    }

    private void UpdateLighting()
    {
        // Smoothly update the light's intensity and color based on the time of day
        if (directionalLight != null)
        {
            Color targetColor = dayLightColor;
            float targetIntensity = 1f;

            if (currentTimeOfDay < 0.25f)  // Sunrise
            {
                targetColor = sunriseLightColor;
                targetIntensity = 0.5f;
            }
            else if (currentTimeOfDay < 0.5f)  // Daytime
            {
                targetColor = dayLightColor;
                targetIntensity = 1f;
            }
            else if (currentTimeOfDay < 0.75f)  // Sunset
            {
                targetColor = sunsetLightColor;
                targetIntensity = 0.8f;
            }
            else  // Nighttime
            {
                targetColor = nightLightColor;
                targetIntensity = 0.2f;
            }

            directionalLight.color = Color.Lerp(directionalLight.color, targetColor, Time.deltaTime * transitionSpeed);
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, targetIntensity, Time.deltaTime * transitionSpeed);

            // Simulate the sun's movement by rotating the light based on time of day
            directionalLight.transform.rotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170f, 0f);
        }
    }

    private void UpdateTemperature()
    {
        // Change the in-game temperature based on day/night cycle
        float currentTemperature = IsDaytime ? dayTemperature : nightTemperature;

        // You can now use currentTemperature to affect player behavior, resource consumption, etc.
        // For example, if the temperature is too low at night, the player might lose warmth.
        Debug.Log($"Current Temperature: {currentTemperature}°C");
    }
}
