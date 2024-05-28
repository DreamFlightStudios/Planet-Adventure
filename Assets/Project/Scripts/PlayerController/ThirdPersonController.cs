using UnityEngine;
using UnityEngine.Windows;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _gravity;
    [SerializeField] private Transform _cameraContainer;
    [SerializeField] private Animator _animator;

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerAiming _playerAiming;

    private Vector3 _movementDirection;
    private Vector2 _input;

    private Vector2 _smoothInputVelocity;
    private float _smoothRotationVelocity;

    private bool _isAiming;

    private Vector3 _inputDirection;
    private float _velocity;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAiming = GetComponent<PlayerAiming>();
    }

    private void Update()
    {
        Vector2 input = _playerInput.Player.MovementInput.ReadValue<Vector2>();
        _input = Vector2.SmoothDamp(_input, input, ref _smoothInputVelocity, _acceleration);
        _input = input;

        if (!_characterController.isGrounded)
        {
           // _movementDirection.y -= _gravity * Time.deltaTime;
        }

        if (_isAiming)
            AimMove();
        else
            Move();

        Debug.Log(input);
        Debug.Log(_input);
        Debug.Log(_movementDirection);
    }

    private void Move()
    {
        Vector2 input = _playerInput.Player.MovementInput.ReadValue<Vector2>();
        float forvardInput = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));
        _inputDirection = new Vector3(input.x, 0f, input.y);

        if (_velocity < 1f && forvardInput > 0f) _velocity += Time.deltaTime * _acceleration;

        if (_velocity > 0f && forvardInput == 0f) _velocity -= Time.deltaTime * _acceleration;

        if (_inputDirection.sqrMagnitude > 0f) Rotate();

        _movementDirection *= _velocity * _walkingSpeed;

        if (!_characterController.isGrounded)
        {
            _movementDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_movementDirection);
    }

    private void Rotate()
    {
        float rotationAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + _cameraContainer.eulerAngles.y;
        float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothRotationVelocity, _rotationSpeed);

        _movementDirection = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
        transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);
    }

    private void AimMove()
    {
        _movementDirection = _input.x * transform.right + _input.y * transform.forward;

        float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cameraContainer.eulerAngles.y, ref _smoothRotationVelocity, _rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);

        _animator.SetFloat("AimingDiractionX", _movementDirection.x);
        _animator.SetFloat("AimingDiractionY", -_movementDirection.z);
    }

    private void OnAimed(bool isAimed)
    {
        _isAiming = isAimed;
        _animator.SetBool("IsAiming", _isAiming);
    }

    private void OnEnable() => _playerAiming.Aimed += OnAimed;

    private void OnDisable() => _playerAiming.Aimed -= OnAimed;
}
