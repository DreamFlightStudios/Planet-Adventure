public class JumpingState : GroundedState
{
    public JumpingState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter()
    {
        base.Enter();

        AgentMover.Jump(Config.MovementInfo.JumpForce);
        Data.IsJumping = true;
    }

    public override void Exit()
    { 
        base.Exit();
        Data.IsJumping = false;
    }
}