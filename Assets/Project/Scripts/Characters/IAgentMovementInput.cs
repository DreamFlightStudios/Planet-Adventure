using System;
using UnityEngine;

public interface IAgentMovementInput
{
    Vector2 MovementInput { get; }
    event Action<bool> SprintInput;
    event Action<bool> CrouchInput;
    event Action<bool> JumpInput;
}