using UnityEngine;
using Zenject;

[RequireComponent(typeof(PauseController))]
public class CameraController : Configurable, IPausable
{
    public bool IsPause { get; private set; }

    [SerializeField] private GameplayCameraConfig _config;
    private InputSystem _input;

    private float _xRotation;
    private float _yRotation;
    private float _sensivity;

    [Inject]
    private void Construct(InputSystem input)
        => _input = input;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause()
        => IsPause = true;

    public void OnResume()
        => IsPause = false;

    protected override void OnConfigurated(UserSettingsData settings)
    {
        base.OnConfigurated(settings);
        _sensivity = settings.Sensivity;
    }

    private void Update()
    {
        if (IsPause == false)
            RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 input = _input.Player.Look.ReadValue<Vector2>() * _sensivity * Time.deltaTime;

        _xRotation -= input.y;
        _yRotation += input.x;

        _xRotation = Mathf.Clamp(_xRotation, _config.MinMaxRotationX.x, _config.MinMaxRotationX.y);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}