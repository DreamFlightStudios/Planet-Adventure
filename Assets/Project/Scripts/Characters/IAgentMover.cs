using UnityEngine;

public interface IAgentMover
{
    Vector3 CurrentVelocity { get; }
    void Move(Vector2 input, float speed);
    void Jump(float force);
}