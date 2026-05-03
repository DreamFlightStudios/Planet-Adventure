using System.Collections;
using UnityEngine;

public class AgentStateMachine : MonoBehaviour, IAgentStateMachine
{
    public IReadonlyAgentStateMachineData Data => _data;

    [SerializeField] private string _defaultStateName;
    [SerializeField] private AgentView _view;
    [SerializeField] private AgentMovementInfo _config;
    
    private AgentStateMachineData _data = new();
    private Coroutine _transitionCoroutine;

    private void Start()
    {
        GetAllStates();
        
        if (!string.IsNullOrEmpty(_defaultStateName) && _data.States.ContainsKey(_defaultStateName))
        {
            SwitchState(_defaultStateName);
        }
    }
    
    private void FixedUpdate()
    {
        if (_data.CurrentState != null && !_data.IsTransitioning)
        {
            _data.CurrentState.UpdateState();
        }
    }
    
    public void SwitchState<T>() where T : IAgentState
    {
        foreach (var state in _data.States.Values)
        {
            if (state is T)
            {
                SwitchState(state.Data.StateName);
                return;
            }
        }
        
        Debug.LogError($"State of type {typeof(T).Name} not found");
    }
    
    public void SwitchState(string stateName)
    {
        if (_data.IsTransitioning)
        {
            Debug.LogWarning($"Already transitioning, ignoring switch to {stateName}");
            return;
        }
        
        if (!_data.TryGetState(stateName, out var newState))
        {
            Debug.LogError($"State {stateName} not found!");
            return;
        }
        
        if (_data.CurrentState == newState)
        {
            Debug.LogWarning($"Already in state {stateName}");
            return;
        }
        
        if (_transitionCoroutine != null)
            StopCoroutine(_transitionCoroutine);
        
        _transitionCoroutine = StartCoroutine(TransitionToState(newState));
    }
    
    private IEnumerator TransitionToState(IAgentState newState)
    {
        _data.IsTransitioning = true;
        
        if (_data.CurrentState != null)
        {
            _data.CurrentState.Exit();
            yield return null;
        }
        
        _data.SetCurrentState(newState);
        _data.CurrentState.Enter();
        
        _data.IsTransitioning = false;
        _transitionCoroutine = null;
    }
    
    private void GetAllStates()
    {
        var foundStates = GetComponents<IAgentState>();
    
        foreach (var state in foundStates)
        {
            Debug.Log(state);
            if (string.IsNullOrEmpty(state.Data.StateName))
            {
                Debug.LogError($"State on {gameObject.name} has no name!");
                continue;
            }
            
            if (!_data.States.ContainsKey(state.Data.StateName))
            {
                _data.AddState(state.Data.StateName, state);
                state.Initialize(_data, _view, _config);
            }
            else
            {
                Debug.LogWarning($"Duplicate state name: {state.Data.StateName}");
            }
        }
        
        if (_data.States.Count == 0)
            Debug.LogError($"No states found on {gameObject.name}!");
    }
}