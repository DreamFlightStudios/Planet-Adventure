using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInput : MonoBehaviour, IAgentMovementInput
{
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public event Action<bool> SprintInput;
    public event Action<bool> JumpInput;
    public event Action<bool> CrouchingInput;

    [SerializeField] private AgentConfig _config;
    private InputSystem _input;

    [Inject]
    private void Construct(InputSystem input) => _input = input;

    private void OnLook(InputAction.CallbackContext context) 
        => LookInput = context.ReadValue<Vector2>();

    private void FixedUpdate()
    {
        MovementInput = _input.Player.Move.ReadValue<Vector2>();
    }

    private void OnSprint(InputAction.CallbackContext context) 
        => SprintInput?.Invoke(context.ReadValueAsButton());

    private void OnCrouch(InputAction.CallbackContext context)
        => CrouchingInput?.Invoke(context.ReadValueAsButton());


    private void OnJump(InputAction.CallbackContext context)
        => JumpInput?.Invoke(context.ReadValueAsButton());


    private void OnEnable()
    {
        _input.Player.Sprint.performed += OnSprint;
        _input.Player.Look.performed += OnLook;
        _input.Player.Crouch.performed += OnCrouch;

        _input.Player.Sprint.canceled += OnSprint;
        _input.Player.Look.canceled += OnLook;

        _input.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _input.Player.Sprint.performed -= OnSprint;
        _input.Player.Look.performed -= OnLook;
        _input.Player.Crouch.performed -= OnCrouch;

        _input.Player.Sprint.canceled -= OnSprint;
        _input.Player.Look.canceled -= OnLook;

        _input.Player.Jump.performed -= OnJump;
    }
}