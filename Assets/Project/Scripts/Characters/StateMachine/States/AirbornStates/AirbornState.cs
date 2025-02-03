public abstract class AirbornState : MovementState
{
    public AirbornState(IStateSwitcher switcher, StateMachineData data, AgentController agent) : base(switcher, data, agent) { }

    public override void Enter() 
        => Data.IsFalling = true;

    public override void Exit() 
        => Data.IsFalling = false;
}