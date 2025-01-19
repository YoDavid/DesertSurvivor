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
            // Handle camera rotation on the Y-axis only
            _yRotation += Input.GetAxis("Mouse X") * _yRotationSpeed * Time.deltaTime;
        }

        // Calculate camera position
        Vector3 direction = Quaternion.Euler(0f, _yRotation, 0f) * Vector3.forward;
        Vector3 position = _player.position - direction * _currentDistance;
        position.y = _player.position.y + _height;

        // Set camera position and rotation
        transform.position = position;
        transform.LookAt(_player);
    }
}
