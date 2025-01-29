using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _player;

    [Header("Distance")]
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _zoomSpeed;

    [Header("Height")]
    [SerializeField] private float _height;

    [Header("Rotation")]
    [SerializeField] private float _yRotation;
    [SerializeField] private float _yRotationSpeed;

    [Header("Collision")]
    [SerializeField] private LayerMask collisionMask; // LayerMask for terrain and objects
    private bool _isRotating;

    private void Update()
    {
        // Handle zoom
        _currentDistance -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _currentDistance = Mathf.Clamp(_currentDistance, _minDistance, _maxDistance);

        // Check for mouse button input
        _isRotating = Input.GetMouseButton(0);
    }

    private void LateUpdate()
    {
        if (_isRotating)
        {
            // Handle camera rotation on the Y-axis only
            _yRotation += Input.GetAxis("Mouse X") * _yRotationSpeed * Time.deltaTime;
        }

        // Calculate desired camera position
        Vector3 direction = Quaternion.Euler(0f, _yRotation, 0f) * Vector3.forward;
        Vector3 desiredPosition = _player.position - direction * _currentDistance;
        desiredPosition.y = _player.position.y + _height;

        // Raycast to check for collisions
        RaycastHit hit;
        if (Physics.Raycast(_player.position, (desiredPosition - _player.position).normalized, out hit, _currentDistance, collisionMask))
        {
            // Move camera closer to the hit point to prevent clipping
            float hitDistance = Mathf.Clamp(hit.distance - 0.2f, _minDistance, _maxDistance);
            desiredPosition = _player.position - direction * hitDistance;
            desiredPosition.y = _player.position.y + _height;
        }

        // Set camera position and rotation
        transform.position = desiredPosition;
        transform.LookAt(_player);
    }
}
