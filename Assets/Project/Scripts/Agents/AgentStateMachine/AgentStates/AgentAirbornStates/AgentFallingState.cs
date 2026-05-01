public class AgentFallingState : AgentAirbornState
{
    public override void Enter()
    {
        base.Enter();
        View.SetBoolParametrValue(AgentAnimationKey.Falling, true);
    }

    public override void Exit()
    {
        base.Exit();
        View.SetBoolParametrValue(AgentAnimationKey.Falling, false);
    }
}