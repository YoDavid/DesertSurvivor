using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStatsManager))]
public class PlayerStatsManagerEditor : Editor
{
    // OnInspectorGUI is called when rendering the inspector
    public override void OnInspectorGUI()
    {
        // Draw the default inspector fields for PlayerStatsManager
        DrawDefaultInspector();

        // Get the reference to the target script
        PlayerStatsManager playerStats = (PlayerStatsManager)target;

        // Add custom buttons for testing DrinkWater and EatFood methods
        GUILayout.Space(10);

        if (GUILayout.Button("Drink Water (20)"))
        {
            playerStats.DrinkWater(20f);  // Call the DrinkWater method with 20 as the amount
        }

        if (GUILayout.Button("Eat Food (20)"))
        {
            playerStats.EatFood(20f);  // Call the EatFood method with 20 as the amount
        }
    }
}
