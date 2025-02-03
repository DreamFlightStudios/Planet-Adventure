using System.Collections.Generic;
using System;

public abstract class State
{
    public event Action<Type> OnTransition;
    protected List<ITransitionRule> TransitionRules = new();

    public void AddTransition(ITransitionRule rule) => TransitionRules.Add(rule);

    public abstract void Enter();

    public abstract void Exit();

    public void Update(float deltaTime)
    {
        if (ShouldTransition(deltaTime))
            return;

        StateUpdate(deltaTime);
    }

    protected abstract void StateUpdate(float deltaTime);

    private bool ShouldTransition(float deltaTime)
    {
        foreach (ITransitionRule rule in TransitionRules)
        {
            if (rule.ShouldTransition(deltaTime))
            {
                OnTransition?.Invoke(rule.NextState);
                return true;
            }
        }
        return false;
    }
}