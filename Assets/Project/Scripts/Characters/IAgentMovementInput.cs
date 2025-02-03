using System;
using UnityEngine;

public interface IAgentMovementInput
{
    public Vector2 MovementInput { get; }
    public event Action<bool> SprintInput;
    public event Action<bool> CrouchingInput;
    public event Action<bool> JumpInput;
}