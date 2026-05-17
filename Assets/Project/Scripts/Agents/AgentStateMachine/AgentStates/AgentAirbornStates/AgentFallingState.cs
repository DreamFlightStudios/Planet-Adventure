public class AgentFallingState : AgentAirbornState
{
    public override void Enter()
    {
        base.Enter();
        View.SetBoolParametrValue(AgentAnimationKey.Falling, true);
    }

    public override void Exit()
    {
        View.SetBoolParametrValue(AgentAnimationKey.Falling, false);
        base.Exit();
    }
}