public class CrouchingState : GroundedState
{
    public CrouchingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsCrouching = true;
        Data.Speed = Config.CrouchingSpeed;

        View.OnCrouching(true);
    }

    public override void Exit()
    {
        base.Exit();

        Data.IsCrouching = false;
        View.OnCrouching(false);
    }
}