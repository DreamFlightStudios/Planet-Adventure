using System.Collections.Generic;

public interface IAgentStateMachine
{
    IReadonlyAgentStateMachineData Data { get; }
    void SetStatesRestrictions(string defaultState, HashSet<string> forbiddenStates, bool isActive);
    void SwitchState<T>() where T : IAgentState;
    void SwitchState(string stateName);
}
