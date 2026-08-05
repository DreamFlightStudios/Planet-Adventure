public class AgentWalkingState : AgentGroundedState
{
    public override void Enter() 
    {
        base.Enter();
        StateMachineData.SetCurrentSpeed(Config.WalkingSpeed);
    }
}