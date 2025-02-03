public abstract class GroundedState : MovementState
{
    public GroundedState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        Input.SprintInput += OnSprintKeyPressed;
        Input.CrouchingInput += OnCrouchKeyPressed;
        Input.JumpInput += OnJumpKeyPressed;
    }

    public override void Exit()
    {
        Input.SprintInput -= OnSprintKeyPressed;
        Input.CrouchingInput -= OnCrouchKeyPressed;
        Input.JumpInput -= OnJumpKeyPressed;
    }

    public override void Update()
    {
        base.Update();

        if (!IsGrounded)
            StateSwitcher.SwitchState<FallingState>();
    }

    private void OnSprintKeyPressed(bool isPressed)
    {
        if (isPressed && Data.IsRunning == false && Input.MovementInput.sqrMagnitude > 0.0f)
            StateSwitcher.SwitchState<SprintingState>();
        else 
            StateSwitcher.SwitchState<WalkingState>();
    }

    private void OnJumpKeyPressed(bool isPressed)
    {
        if (isPressed && Data.IsJumping == false)
            StateSwitcher.SwitchState<JumpingState>();
    }

    private void OnCrouchKeyPressed(bool isPressed)
    {
        if (Data.IsRunning) return;

        if (isPressed && Data.IsCrouching == false)
            StateSwitcher.SwitchState<CrouchingState>();
        else 
            StateSwitcher.SwitchState<WalkingState>();
    }
}