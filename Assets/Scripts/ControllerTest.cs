using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;

    public float moveSpeed = 3f; // Movement speed
    public float rotationSpeed = 700f; // Rotation speed

    private float speed = 0f; // Speed (0 = Idle, 1 = Walking, 2 = Running)
    private float movementDirectionX = 0f; // Horizontal movement (left-right)
    private float movementDirectionY = 0f; // Vertical movement (forward-backward)
    private bool isWalking = false; // Whether the player is walking or not
    private bool isRunning = false; // Whether the player is running or not

    void Update()
    {
        // Get input for movement (W, A, S, D)
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

        // Check for running input (Shift key)
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Determine movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // If moving, move the player
        if (moveDirection.magnitude >= 0.1f)
        {
            // Set movement direction based on input
            movementDirectionX = horizontal; // Left/Right
            movementDirectionY = vertical; // Forward/Backward

            // Rotate the player smoothly towards the direction of movement
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Move the player
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Set speed based on whether running or walking
            speed = isRunning ? 2f : 1f; // Running sets speed to 2, walking sets speed to 1
            isWalking = true;
        }
        else
        {
            speed = 0f; // Idle
            isWalking = false;
        }

        // Set animation parameters
        animator.SetFloat("Speed", speed); // Set Speed (0 = Idle, 1 = Walking, 2 = Running)
        animator.SetFloat("MovementDirectionX", movementDirectionX); // Set horizontal direction (Left-Right)
        animator.SetFloat("MovementDirectionY", movementDirectionY); // Set vertical direction (Forward-Backward)
        animator.SetBool("IsWalking", isWalking); // Set walking state for transitions
        animator.SetBool("IsRunning", isRunning); // Set running state for transitions
    }
}
