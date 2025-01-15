using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    // Camera offsets
    [SerializeField] private float distance = 10f;
    [SerializeField] private float height = 5f;

    // Camera rotation
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    [SerializeField] private float xRotationSpeed;
    [SerializeField] private float yRotationSpeed;

    // Mouse input flag
    private bool isRotating = false;

    void Update()
    {
        // Check for mouse button input
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
    }

    void LateUpdate()
    {
        if (isRotating)
        {
            // Handle camera rotation
            xRotation += Input.GetAxis("Mouse Y") * xRotationSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation
            yRotation += Input.GetAxis("Mouse X") * yRotationSpeed * Time.deltaTime;
        }

        // Calculate camera position
        Vector3 direction = Quaternion.Euler(xRotation, yRotation, 0f) * Vector3.forward;
        Vector3 position = player.position - direction * distance;
        position.y = player.position.y + height;

        // Set camera position and rotation
        transform.position = position;
        transform.LookAt(player);
    }
}