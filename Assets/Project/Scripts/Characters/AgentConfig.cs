using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agent Config", menuName = "Configs/Gameplay/Create Agent Config")]
public class AgentConfig : ScriptableObject
{
    [field: SerializeField] public AgentMovementInfo MovementInfo;
}

[Serializable]
public struct AgentMovementInfo
{
    [field: SerializeField] public float WalkingSpeed { get; private set; }
    [field: SerializeField] public float RuningSpeed { get; private set; }
    [field: SerializeField] public float CrouchingSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
}