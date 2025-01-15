using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;

    [Header("Speed Transitions")]
    [SerializeField] private float transitionSpeed; // Controls how quickly speed transitions
    [SerializeField] private float gatheringTransitionSpeed; // Speed for blending between idle and gathering

    [Header("Current Values")]
    [SerializeField] private float currentSpeed; // Tracks the current interpolated speed
    [SerializeField] private float currentBlend; // Tracks the current blend value (Idle to Gathering) 

    public void UpdateAnimatorParameters(Vector3 moveDirection, bool isRunning, bool isGathering)
    {
        // Calculate the target speed
        float targetSpeed = isRunning ? 2f : moveDirection.magnitude >= 0.1f ? 1f : 0f;

        // Smoothly interpolate towards the target speed for gradual transitions
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, transitionSpeed * Time.deltaTime);

        // Determine if the player is walking
        bool isWalking = moveDirection.magnitude >= 0.1f;

        // Smoothly transition the blend value for idle/gathering
        float targetBlend = isGathering ? 1f : 0f;
        currentBlend = Mathf.MoveTowards(currentBlend, targetBlend, gatheringTransitionSpeed * Time.deltaTime);

        // Set parameters for movement
        _animator.SetFloat("Speed", currentSpeed); // Gradually change Speed for smoother transitions
        _animator.SetFloat("MovementDirectionX", moveDirection.x);
        _animator.SetFloat("MovementDirectionY", moveDirection.z);
        _animator.SetBool("IsWalking", isWalking);
        _animator.SetBool("IsRunning", isRunning);
        _animator.SetBool("IsGathering", isGathering);

        // Gradually change the "Blend" parameter between Idle and Gathering (0 to 1)
        _animator.SetFloat("Blend", currentBlend);
    }
}
