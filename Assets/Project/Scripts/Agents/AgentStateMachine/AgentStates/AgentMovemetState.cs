public abstract class AgentMovementState : AgentState
{
    public override void Enter()
    {
        MovementInput.SprintInput += OnSprintKeyPressed;
        MovementInput.CrouchInput += OnCrouchKeyPressed;
    }

    public override void Exit()
    {
        MovementInput.SprintInput -= OnSprintKeyPressed;
        MovementInput.CrouchInput -= OnCrouchKeyPressed;
    }

    public override void UpdateState()
    {
        Mover.Move(MovementInput.SmoothMovementInput, StateMachineData.CurrentStateSpeed);
        View.SetFloatParametrValue(AgentAnimationKey.MovementVelocity, MovementInput.SmoothMovementInput.sqrMagnitude);
    }

    private void OnSprintKeyPressed(bool isPressed)
    {
        if (isPressed == true && Mover.IsGrounded && StateMachineData.CurrentState is not AgentSprintingState)
            StateMachine.SwitchState<AgentSprintingState>();
        else if (isPressed == false && Mover.IsGrounded)
            StateMachine.SwitchState<AgentMovementState>();
    }

    private void OnCrouchKeyPressed(bool isPressed)
    {
        if (isPressed == true && Mover.IsGrounded && StateMachineData.CurrentState is not AgentCrouchingState)
            StateMachine.SwitchState<AgentCrouchingState>();
        else if (isPressed == false && Mover.IsGrounded)
            StateMachine.SwitchState<AgentMovementState>();
    }
}