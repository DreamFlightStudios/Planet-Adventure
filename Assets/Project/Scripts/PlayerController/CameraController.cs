using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensivity;
    [SerializeField] private Vector2Int _minMaxRotationX;

    private float _xRotation;
    private float _yRotation;

    private PlayerInput _playerInput;

    private void Awake() => _playerInput = new PlayerInput();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() => RotateCamera();

    private void RotateCamera()
    {
        Vector2 input = _playerInput.Player.LookInput.ReadValue<Vector2>() * _sensivity * Time.deltaTime;

        _xRotation -= input.y;
        _yRotation += input.x;

        _xRotation = Mathf.Clamp(_xRotation, _minMaxRotationX.x, _minMaxRotationX.y);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }

    private void OnDisable() => _playerInput.Disable();

    private void OnEnable() => _playerInput.Enable();
}
