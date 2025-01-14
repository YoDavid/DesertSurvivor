using UnityEngine;

public class AnimationDebug : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;

    [SerializeField] private float speed = 0f; // Speed for animations (0 = Idle, 1 = Walking, 2 = Running)
    private float movementDirection = 0f; // Direction for animation blending
    private bool isWalking = false; // Whether the player is walking
    private bool isRunning = false; // Whether the player is running
    private bool isGathering = false; // Whether the player is gathering
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isGrounded;

    private float targetSpeed = 0f; // Target speed for smooth transitions
    private float blendSpeed = 2f; // Speed of smooth transition for blending

    void Update()
    {
        // Check if the character is grounded
        isGrounded = controller.isGrounded;

        // Get input for movement (W, A, S, D)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Check for running input
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Determine movement and speed
        if (moveDirection.magnitude >= 0.1f)
        {
            isWalking = true;
            movementDirection = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            // Set target speed:
            // - 2 when Shift is held (Running)
            // - 1 otherwise (Walking)
            targetSpeed = isRunning ? 2f : 1f;
        }
        else
        {
            isWalking = false;
            targetSpeed = 0f; // Idle
        }

        // Smoothly transition speed for animations
        speed = Mathf.MoveTowards(speed, targetSpeed, blendSpeed * Time.deltaTime);

        // Apply movement
        controller.Move(moveDirection * targetSpeed * 2f * Time.deltaTime);

        // Apply gravity manually
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the player grounded (can adjust)
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Handle Gathering input (press G to start gathering)
        isGathering = Input.GetKey(KeyCode.G);

        // Pass parameters to Animator
        animator.SetFloat("Speed", speed); // Update Speed for blending
        animator.SetFloat("MovementDirection", movementDirection); // Update movement direction
        animator.SetBool("IsWalking", isWalking); // Update walking state
        animator.SetBool("IsRunning", isRunning); // Update running state
        animator.SetBool("IsGathering", isGathering); // Update gathering state
    }
}