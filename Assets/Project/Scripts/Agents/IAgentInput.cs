using System;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementInput { get; }
    Vector2 SmoothMovementInput { get; }

    event Action<bool> SprintInput;
    event Action<bool> CrouchInput;
}