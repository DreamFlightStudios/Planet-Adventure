public class SprintingState : GroundedState
{
    public SprintingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsSprinting = true;
        Data.CurrentSpeed = Config.MovementInfo.RuningSpeed;

        View.SetBoolParametrValue(AgentAnimationKey.Sprinting, true);
    }

    public override void Exit()
    {
        base.Exit();

        Data.IsSprinting = false;
        View.SetBoolParametrValue(AgentAnimationKey.Sprinting, false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.MovementInput.sqrMagnitude <= 0.0f)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
