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
    [SerializeField] private float _xRotation;
    [SerializeField] private float _yRotation;
    [SerializeField] private float _xRotationSpeed;
    [SerializeField] private float _yRotationSpeed;

    [Header("State")]
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
            // Handle camera rotation
            _xRotation += Input.GetAxis("Mouse Y") * _xRotationSpeed * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Limit vertical rotation
            _yRotation += Input.GetAxis("Mouse X") * _yRotationSpeed * Time.deltaTime;
        }

        // Calculate camera position
        Vector3 direction = Quaternion.Euler(_xRotation, _yRotation, 0f) * Vector3.forward;
        Vector3 position = _player.position - direction * _currentDistance;
        position.y = _player.position.y + _height;

        // Set camera position and rotation
        transform.position = position;
        transform.LookAt(_player);
    }
}