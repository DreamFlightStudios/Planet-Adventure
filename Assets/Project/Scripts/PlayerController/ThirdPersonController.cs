using UnityEngine;
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
    [SerializeField] private Vector2 _movementDeadZone;

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerAiming _playerAiming;

    private Vector3 _movementDirection;
    private Vector2 _input;
    float _forvardInput;

    private bool _isAiming;

    private float _smoothRotationVelocity;
    private Vector2 _smoothInputVelocity;
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
        _movementDirection *= _isAiming ? _aimingSpeed : _walkingSpeed;

        Debug.Log(_input);

        if (!_characterController.isGrounded)
        {
           _movementDirection.y -= _gravity * Time.deltaTime;
        }

        if (_isAiming)
            AimMove();
        else
            Move();

        _characterController.Move(_movementDirection * Time.deltaTime);
    }

    private void Move()
    {
        _forvardInput = Mathf.Abs(_input.x) + Mathf.Abs(_input.y);
        if (_input.sqrMagnitude > _movementDeadZone.sqrMagnitude) 
        {
            float rotationAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + _cameraContainer.eulerAngles.y;
            float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothRotationVelocity, _rotationSpeed);

            _movementDirection = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);
        }

        _animator.SetFloat("ForvardVelocity", _forvardInput);
    }

    private void AimMove()
    {
        _movementDirection = _input.x * transform.right + _input.y * transform.forward;

        float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cameraContainer.eulerAngles.y, ref _smoothRotationVelocity, _rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);

        _animator.SetFloat("VelocityY", _input.x);
        _animator.SetFloat("VelocityX", _input.y);
    }

    private void OnAimed(bool isAimed)
    {
        _isAiming = isAimed;
        _animator.SetBool("IsAiming", _isAiming);
    }

    private void OnEnable() => _playerAiming.Aimed += OnAimed;

    private void OnDisable() => _playerAiming.Aimed -= OnAimed;
}
