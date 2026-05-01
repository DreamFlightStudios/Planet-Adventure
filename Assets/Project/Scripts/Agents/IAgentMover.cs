using UnityEngine;

public interface IAgentMover
{
    bool IsGrounded {  get; }
    Vector3 Velocity { get; }
    void Move(Vector3 diraction, float speed);
    void Stop();
}