using UnityEngine;

[CreateAssetMenu(fileName = "New Agent Movement Info", menuName = "Configs/Gameplay/Create Agent Movement Info")]
public class AgentMovementInfo : ScriptableObject
{
    [Header("Movemet Speed")]
    [field: SerializeField] public float WalkingSpeed { get; private set; }
    [field: SerializeField] public float SprintingSpeed { get; private set; }
    [field: SerializeField] public float CrouchingSpeed { get; private set; }

    [Header("Movemet Speed")]
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Deceleration { get; private set; }
}