using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Transform _cameraTransform; // Reference to the camera's transform

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Movement Data")]
    [SerializeField] private Vector3 moveDirection;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimator>();
        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform; // Fallback to main camera if not assigned
        }
    }

    void Update()
    {
        // Get player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isGathering = Input.GetKey(KeyCode.G);

        // Calculate movement direction relative to the camera's orientation
        Vector3 forward = _cameraTransform.forward;
        forward.y = 0; // Ignore vertical component
        forward.Normalize();

        Vector3 right = _cameraTransform.right;
        right.y = 0; // Ignore vertical component
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Handle rotation and movement
        if (moveDirection.magnitude >= 0.1f)
        {
            // Calculate target angle
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Move the player
            _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Update animation parameters
        _animator.UpdateAnimatorParameters(moveDirection, isRunning, isGathering);
    }
}
