using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStatsManager))]
public class PlayerStatsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Get the target script reference
        PlayerStatsManager playerStats = (PlayerStatsManager)target;

        // Add buttons for debugging DrinkWater and EatFood
        GUILayout.Space(10);
        if (GUILayout.Button("Drink Water (20)"))
        {
            playerStats.DrinkWater(20f);  // Simulate drinking 20 units of water
        }

        if (GUILayout.Button("Eat Food (20)"))
        {
            playerStats.EatFood(20f);  // Simulate eating 20 units of food
        }
    }
}
