using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 700f;

    private CharacterController controller;
    private PlayerAnimator animator;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isGathering = Input.GetKey(KeyCode.G);

        // Calculate move direction
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Handle rotation and movement
        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Update animation parameters (passing moveDirection, isRunning, isGathering)
        animator.UpdateAnimatorParameters(moveDirection, isRunning, isGathering);
    }
}
