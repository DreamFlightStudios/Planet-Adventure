public class AgentFallingState : AgentAirbornState
{
    public override void Enter()
    {
        base.Enter();
        View.SwitchRagDoll(true);
    }

    public override void Exit()
    {
        base.Exit();
        View.SwitchRagDoll(false);
    }
}