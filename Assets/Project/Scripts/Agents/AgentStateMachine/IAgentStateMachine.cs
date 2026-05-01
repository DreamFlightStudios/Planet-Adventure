public interface IAgentStateMachine
{
    IReadonlyAgentStateMachineData Data { get; }
    void SwitchState<T>() where T : IAgentState;
    void SwitchState(string stateName);
}
