using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _controller;

    [Header("Gravity")]
    [SerializeField] private float _gravity = -9.81f;

    [Header("Velocity")]
    [SerializeField] private Vector3 _velocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}