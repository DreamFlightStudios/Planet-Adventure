using UnityEngine;

public abstract class AgentState : MonoBehaviour, IAgentState
{
    [field: SerializeField] public string StateName { get; private set; }
    IReadonlyAgentStateData IAgentState.Data => Data;

    protected IAgentStateMachine StateMachine { get; private set; }
    protected IAgentInput MovementInput { get; private set; }
    protected IAgentMover Mover { get; private set; }
    protected AgentView View { get; private set; }
    protected AgentStateData Data { get; private set; } 
    protected AgentStateMachineData StateMachineData { get; private set; }
    protected AgentMovementInfo Config { get; private set; }

    private void Start()
    {
        StateMachine = GetComponent<IAgentStateMachine>();
        MovementInput = GetComponent<IAgentInput>();
        Mover = GetComponent<IAgentMover>();

        Data = new AgentStateData(StateName);
    }

    public virtual void Initialize(AgentStateMachineData stateMachineData, AgentView view, AgentMovementInfo config)
    {
        StateMachineData = stateMachineData;
        View = view;
        Config = config;
    }
    
    public abstract void Enter();
    public abstract void Exit();    
    public abstract void UpdateState();
    
    protected void SwitchToSelf() => StateMachine.SwitchState(StateName);
    protected void SwitchTo<T>() where T : IAgentState => StateMachine.SwitchState<T>();
    protected void SwitchTo(string stateName) => StateMachine.SwitchState(stateName);
}