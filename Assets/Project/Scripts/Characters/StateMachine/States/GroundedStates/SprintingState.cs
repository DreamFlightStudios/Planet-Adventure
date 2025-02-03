public class SprintingState : GroundedState
{
    public SprintingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        Data.IsRunning = true;
        Data.Speed = Config.RuningSpeed;

        View.OnRunning(true);
    }

    public override void Exit()
    {
        base.Exit();

        Data.IsRunning = false;
        View.OnRunning(false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.MovementInput.sqrMagnitude <= 0.0f)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
