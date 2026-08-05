using System.Collections.Generic;
using UnityEngine;

public class AgentStateMachineData : IReadonlyAgentStateMachineData
{   
    public IReadonlyAgentStateData CurrentStateData => CurrentState?.Data;

    public IAgentState CurrentState { get; private set; }
    public float CurrentStateSpeed { get; private set; }
    public bool IsTransitioning { get; set; }
    public bool HasForbiddenStates { get; set; }

    public Dictionary<string, IAgentState> States = new();
    public HashSet<string> ForbiddenStates = new();

    public void SetCurrentState(IAgentState state)
    {
        if (state == null)
        {
            Debug.LogError("Attempted to set null state!");
            return;
        }
        
        if (CurrentState == state)
        {
            Debug.LogWarning($"State {state.Data.StateName} is already active!");
            return;
        }
        
        CurrentState = state;
    }

    public void SetCurrentSpeed(float speed)
    {
        if (speed < 0f)
        {
            Debug.LogWarning($"Attempted to set negative speed: {speed}. Clamping to 0.");
            CurrentStateSpeed = 0f;
        }
        else
        {
            CurrentStateSpeed = speed;
        }
    }

    public void AddState(string stateName, IAgentState state)
    {
        if (States.ContainsKey(stateName))
        {
            Debug.LogWarning($"State {stateName} already exists!");
            return;
        }
        
        States.Add(stateName, state);
    }

    public bool TryGetState(string stateName, out IAgentState state) 
        => States.TryGetValue(stateName, out state);
}