using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour, IAnimated
{
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _gravity;
    [SerializeField] private Transform _cameraContainer;

    private PlayerInput _playerInput;
    private CharacterController _characterController;

    private Vector3 _inputDirection;
    private Vector3 _movementDirection;

    private float _rotationSmoothVelocity;
    private float _velocity;

    public event Action<float, float> Moved;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;  

    private void Start() => _characterController = GetComponent<CharacterController>();

    private void Update() => Move();

    private void Move()
    {
        Vector2 input = _playerInput.Player.MovementInput.ReadValue<Vector2>();
        float forvardInput = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));
        _inputDirection = new Vector3(input.x, 0f, input.y);

        if (_velocity < 1f && forvardInput > 0f) _velocity += Time.deltaTime * _acceleration;

        if (_velocity > 0f && forvardInput == 0f) _velocity -= Time.deltaTime * _acceleration;

        if (_inputDirection.sqrMagnitude > 0f) Rotate();

        _movementDirection *= _velocity * _walkingSpeed;

        if(!_characterController.isGrounded)
        {
            _movementDirection.y -= _gravity * Time.deltaTime;
        }   

        _characterController.Move(_movementDirection);
        Moved?.Invoke(_velocity, _movementDirection.normalized.y);
    }

    private void Rotate()
    {
        float rotationAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + _cameraContainer.eulerAngles.y;
        float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _rotationSmoothVelocity, _rotationSpeed);

        _movementDirection = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
        transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);
    }
}