public interface IAgentState
{
    IReadonlyAgentStateData Data { get; }
    void Initialize(AgentStateMachineData stateMachineData, AgentMovementInfo config);
    void Enter();
    void Exit();
    void UpdateState();
}