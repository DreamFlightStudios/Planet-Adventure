public class AgentCrouchingState : AgentMovementState
{
    public override void Enter()
    {
        base.Enter();
        StateMachineData.SetCurrentSpeed(Config.CrouchingSpeed);
        View.SetBoolParametrValue(AgentAnimationKey.Crouching, true);
    }

    public override void Exit()
    {
        base.Exit();
        View.SetBoolParametrValue(AgentAnimationKey.Crouching, false);
    }
}