    using UnityEngine;

public class PlayerMover : MonoBehaviour, IAgentMover
{
    public Vector3 CurrentVelocity { get; private set; }

    [SerializeField] private AgentConfig _config;
    [SerializeField] private AgentRoatationStrategy _roatationStrategy;

    private float _targetRotation;
    private float _smoothRotationVelocity;

    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Move(Vector2 input, float speed)
    {
        if (input != Vector2.zero)
        {
            _targetRotation = _roatationStrategy.RotationCalculation(input, transform,
                ref _smoothRotationVelocity, _config.MovementInfo.RotationSpeed, _targetRotation);

            Vector3 direction = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            CurrentVelocity = new Vector3(direction.x, 0.0f, direction.z);

            _rigidbody.linearVelocity = CurrentVelocity * Time.fixedDeltaTime * speed +
                new Vector3(0.0f, _rigidbody.linearVelocity.y, 0.0f);
        }
    }

    public void Jump(float force) 
        => _rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
}