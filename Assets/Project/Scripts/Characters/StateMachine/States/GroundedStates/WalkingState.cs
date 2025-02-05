public class WalkingState : GroundedState
{
    public WalkingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsWalking = true;
        Data.CurrentSpeed = Config.MovementInfo.WalkingSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        Data.IsWalking = false;
    }
}
