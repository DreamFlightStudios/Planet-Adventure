using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameplayCameraConfig _config;

    private float _xRotation;
    private float _yRotation;

    private InputSystem _input;
    private UserData _userData;

    [Inject]
    private void Construct(InputSystem input, SaveLoadController saveLoadController)
    {
        _input = input;
        _userData = saveLoadController.UserData;
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() => RotateCamera();

    private void RotateCamera()
    {
        Vector2 input = _input.Player.Look.ReadValue<Vector2>() * _userData.SettingsData.Sensivity * Time.deltaTime;

        _xRotation -= input.y;
        _yRotation += input.x;

        _xRotation = Mathf.Clamp(_xRotation, _config.MinMaxRotationX.x, _config.MinMaxRotationX.y);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}