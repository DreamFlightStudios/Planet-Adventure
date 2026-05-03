public interface IAgentState
{
    IReadonlyAgentStateData Data { get; }
    void Initialize(AgentStateMachineData stateMachineData, AgentView view, AgentMovementInfo config);
    void Enter();
    void Exit();
    void UpdateState();
}