public abstract class AgentGroundedState : AgentMovementState
{
    public override void UpdateState()
    {
        if (Mover.IsGrounded == false)
            StateMachine.SwitchState<AgentFallingState>();

        base.UpdateState();
    }
}