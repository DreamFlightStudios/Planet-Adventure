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

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerAiming _playerAiming;

    private Vector3 _movementDirection;
    private Vector2 _smoothInput;
    private Vector2 _input;

    private float _smoothRotationVelocity;
    private Vector2 _smoothInputVelocity;

    private bool _isAiming;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAiming = GetComponent<PlayerAiming>();
    }

    private void Update()
    {
        _input = _playerInput.Player.MovementInput.ReadValue<Vector2>();
        _smoothInput = Vector2.SmoothDamp(_smoothInput, _input, ref _smoothInputVelocity, _acceleration);


        if (_isAiming)
            AimMove();
        else
            Move();

        if (!_characterController.isGrounded)
        {
           _movementDirection.y -= _gravity * Time.deltaTime;
        }
        
        _movementDirection *= _isAiming ? _aimingSpeed : _walkingSpeed;
        _characterController.Move(_movementDirection * Time.deltaTime);
    }

    private void Move()
    {
        if (_input.sqrMagnitude > 0) 
        {
            float rotationAngle = Mathf.Atan2(_smoothInput.x, _smoothInput.y) * Mathf.Rad2Deg + _cameraContainer.eulerAngles.y;
            float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothRotationVelocity, _rotationSpeed);

            _movementDirection = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);
        }
        else _movementDirection = Vector3.zero;

        float forvardInput = Mathf.Abs(_smoothInput.x) + Mathf.Abs(_smoothInput.y);
        _animator.SetFloat("ForvardVelocity", forvardInput);
    }

    private void AimMove()
    {
        _movementDirection = _smoothInput.x * transform.right + _smoothInput.y * transform.forward;

        float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cameraContainer.eulerAngles.y, ref _smoothRotationVelocity, _rotationSpeed);
        transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);

        _animator.SetFloat("VelocityY", _smoothInput.x);
        _animator.SetFloat("VelocityX", _smoothInput.y);
    }

    private void OnAimed(bool isAimed)
    {
        _isAiming = isAimed;
        _animator.SetBool("IsAiming", _isAiming);
    }

    private void OnEnable() => _playerAiming.Aimed += OnAimed;

    private void OnDisable() => _playerAiming.Aimed -= OnAimed;
}
