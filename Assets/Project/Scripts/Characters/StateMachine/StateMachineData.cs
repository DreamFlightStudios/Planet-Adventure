using System;
using UnityEngine;

public class StateMachineData
{
    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0) 
                throw new ArgumentOutOfRangeException(nameof(value));

            _speed = value;
        }
    }

    public Vector3 Velocity;
    public Vector2 SmoothInput;

    public bool IsRunning;
    public bool IsWalking;
    public bool IsIdling;
    public bool IsJumping;
    public bool IsFalling;
    public bool IsCrouching;

    private float _speed;
}