using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly AgentController Agent;
    protected readonly StateMachineData Data;
    protected readonly IStateSwitcher StateSwitcher;

    protected bool IsGrounded => Agent.GroundChecker.IsGrounded;
    protected IAgentMovementInput Input => Agent.Input;
    protected IAgentMover AgentMover => Agent.AgentMover;
    protected AgentConfig Config => Agent.Config;
    protected AgentView View => Agent.View;

    public MovementState(IStateSwitcher switcher, StateMachineData data, AgentController agent)
    {
        Agent = agent;
        Data = data;
        StateSwitcher = switcher;
    }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void Update()
    {
        AgentMover.Move(Input.MovementInput, Data.CurrentSpeed);

        Data.SmoothInput = Vector2.Lerp(Data.SmoothInput, Input.MovementInput,
            Config.MovementInfo.Acceleration * Time.fixedDeltaTime);
        View.SetFloatParametrValue(AgentAnimationKey.MovementVelocity, Data.SmoothInput.sqrMagnitude);
    }
}