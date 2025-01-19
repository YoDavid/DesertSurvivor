using UnityEngine;
using TMPro;  // Make sure you have the TMP namespace for TextMeshPro

public class TimeHUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;  // TextMeshPro Text for displaying the time
    private TimeManager timeManager;

    private void Awake()
    {
        // Find the TimeManager in the scene
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        // Update the time on the UI every frame
        if (timeManager != null)
        {
            UpdateTimeUI();
        }
    }

    private void UpdateTimeUI()
    {
        // Format the time in HH:MM format and display it
        string formattedTime = string.Format("{0:D2}:{1:D2}", timeManager.Hours, timeManager.Minutes);
        timeText.text = formattedTime;
    }
}
