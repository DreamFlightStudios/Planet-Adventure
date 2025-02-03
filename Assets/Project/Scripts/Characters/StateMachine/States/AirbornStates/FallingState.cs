public class FallingState : AirbornState
{
    public FallingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsFalling = true;
        View.OnFalling(true);
    }

    public override void Exit()
    {
        base.Exit();

        Data.IsFalling = false;
        View.OnFalling(false);
    }

    public override void Update()
    {
        base.Update();

        if (IsGrounded)
        {
            StateSwitcher.SwitchState<WalkingState>();
        }
    }
}