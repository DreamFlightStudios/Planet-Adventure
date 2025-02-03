using System;

public interface ITransitionRule
{
    bool ShouldTransition(float deltaTime);
    Type NextState { get; }
}