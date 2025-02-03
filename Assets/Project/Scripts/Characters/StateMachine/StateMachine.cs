using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    protected List<IState> _states = new();
    protected IState _currentState;

    public void AddState(IState state) => _states.Add(state);

    public void Initialize()
    {
        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();

        Debug.Log(_currentState.ToString());
    }

    public void Update() => _currentState.Update();
}
