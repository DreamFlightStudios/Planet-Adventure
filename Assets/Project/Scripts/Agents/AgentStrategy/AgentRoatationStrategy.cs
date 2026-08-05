using UnityEngine;

public abstract class AgentRoatationStrategy : MonoBehaviour
{
    [SerializeField] private AgentMovementInfo _config;

    public float RotationCalculation(Vector2 movementInput, Transform agentTransform, ref float rotationVelocity, float RotationSmoothTime, float targetRotation)
    {
        if (movementInput.sqrMagnitude >= _config.RotationInputDeadzone)
        {
            targetRotation = RotationStrategy(movementInput);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                RotationSmoothTime);

            agentTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        return targetRotation;
    }

    protected abstract float RotationStrategy(Vector2 movementInput);
}