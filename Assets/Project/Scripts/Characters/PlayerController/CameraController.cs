using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameplayCameraConfig _config;

    private float _xRotation;
    private float _yRotation;

    private InputSystem _input;

    [Inject]
    private void Construct(InputSystem input) => _input = input;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() => RotateCamera();

    private void RotateCamera()
    {
        Vector2 input = _input.Player.Look.ReadValue<Vector2>() * _config.Sensivity * Time.deltaTime;

        _xRotation -= input.y;
        _yRotation += input.x;

        _xRotation = Mathf.Clamp(_xRotation, _config._minMaxRotationX.x, _config._minMaxRotationX.y);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}