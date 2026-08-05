public class AgentSprintingState : AgentGroundedState
{
    public override void Enter()
    {
        base.Enter();

        StateMachineData.SetCurrentSpeed(Config.SprintingSpeed);
        View.SetBoolParametrValue(AgentAnimationKey.Sprinting, true);
    }

    public override void Exit()
    {
        base.Exit();
        View.SetBoolParametrValue(AgentAnimationKey.Sprinting, false);
    }
}