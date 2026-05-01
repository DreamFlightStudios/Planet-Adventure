public abstract class AgentAirbornState : AgentMovementState
{
    public override void UpdateState()
    {
        base.UpdateState();

        if (Mover.IsGrounded)
        {
            StateMachine.SwitchState<AgentWalkingState>();
        }
    }
}