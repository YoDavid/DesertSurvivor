using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    // Exposed variables for position and rotation in the Inspector
    public Vector3 cameraPositionOffset = new Vector3(0f, 0f, 0f); // Default offset for camera position
    public Vector3 cameraRotation = new Vector3(0f, 0f, 0f); // Default camera rotation (Euler angles)

    void Update()
    {
        // Set the camera's position based on player position and offset
        transform.position = player.position + cameraPositionOffset;

        // Apply rotation (Euler angles from Inspector)
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
