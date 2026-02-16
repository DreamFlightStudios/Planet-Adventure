using UnityEngine;
using Zenject;

public class CameraController : Configurable
{
    [SerializeField] private GameplayCameraConfig _config;
    private InputSystem _input;

    private float _xRotation;
    private float _yRotation;
    private float _sensivity;

    [Inject]
    private void Construct(InputSystem input, SaveLoadController saveLoadController) 
        => _input = input;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() => RotateCamera();

    private void RotateCamera()
    {
        Vector2 input = _input.Player.Look.ReadValue<Vector2>() * _sensivity * Time.deltaTime;

        _xRotation -= input.y;
        _yRotation += input.x;

        _xRotation = Mathf.Clamp(_xRotation, _config.MinMaxRotationX.x, _config.MinMaxRotationX.y);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    protected override void OnConfigurated(UserData data)
    {
        base.OnConfigurated(data);
        _sensivity = data.SettingsData.Sensivity;
    }
}