using System;
using UnityEngine;

public class StateMachineData
{
    public float CurrentSpeed
    {
        get => _currentSpeed;
        set
        {
            if (value < 0) 
                throw new ArgumentOutOfRangeException(nameof(value));

            _currentSpeed = value;
        }
    }

    public Vector2 SmoothInput;
    public bool IsSprinting;
    public bool IsWalking;
    public bool IsJumping;
    public bool IsFalling;
    public bool IsCrouching;

    private float _currentSpeed;
}