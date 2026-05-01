using UnityEngine;

public class PlayerRotationStrategy : AgentRoatationStrategy
{
    [SerializeField] private Transform _camera;

    protected override float RotationStrategy(Vector2 movementInput)
        => Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
}