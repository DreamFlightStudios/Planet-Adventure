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
        if (Mover.IsGrounded == false)
            return;

        if (isPressed && StateMachineData.CurrentState is not AgentSprintingState)
            StateMachine.SwitchState<AgentSprintingState>();
        else
            StateMachine.SwitchState<AgentWalkingState>();
    }

    private void OnCrouchKeyPressed(bool isPressed)
    {
        if (Mover.IsGrounded == false || StateMachineData.CurrentState is AgentSprintingState)
            return;

        if (isPressed && StateMachineData.CurrentState is not AgentCrouchingState)
            StateMachine.SwitchState<AgentCrouchingState>();
        else
            StateMachine.SwitchState<AgentWalkingState>();
    }
}