using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public float transitionSpeed = 2f; // Controls how quickly speed transitions
    public float gatheringTransitionSpeed = 2f; // Speed for blending between idle and gathering

    private float currentSpeed = 0f; // Tracks the current interpolated speed
    private float currentBlend = 0f; // Tracks the current blend value (Idle to Gathering)

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
        animator.SetFloat("Speed", currentSpeed); // Gradually change Speed for smoother transitions
        animator.SetFloat("MovementDirectionX", moveDirection.x);
        animator.SetFloat("MovementDirectionY", moveDirection.z);
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsGathering", isGathering);

        // Gradually change the "Blend" parameter between Idle and Gathering (0 to 1)
        animator.SetFloat("Blend", currentBlend);
    }
}
