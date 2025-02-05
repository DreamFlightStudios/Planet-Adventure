public class CrouchingState : GroundedState
{
    public CrouchingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsCrouching = true;
        Data.CurrentSpeed = Config.MovementInfo.CrouchingSpeed;

        View.SetBoolParametrValue(AgentAnimationKey.Crouching, true);
    }

    public override void Exit()
    {
        base.Exit();

        Data.IsCrouching = false;
        View.SetBoolParametrValue(AgentAnimationKey.Crouching, false);
    }
}