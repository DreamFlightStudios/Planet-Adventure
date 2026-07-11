using UnityEngine;

[RequireComponent (typeof(AgentRoatationStrategy))]
public class PlayerMover : MonoBehaviour, IAgentMover, IPausable
{
    public bool IsGrounded => _groundChecker.IsGrounded;

    public Vector3 Velocity { get; private set; }
    public bool IsPause { get; private set; }

    [SerializeField] private AgentMovementInfo _config;
    [SerializeField] private GroundChecker _groundChecker;

    private Rigidbody _rigidbody;
    private AgentRoatationStrategy _rotationStrategy;

    private float _targetRotation;
    private float _smoothRotationVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rotationStrategy = GetComponent<AgentRoatationStrategy>();
    }

    public void Move(Vector3 direction, float speed)
    {
        if (IsPause) return;

        if (direction != Vector3.zero)
        {
            ApplyMovement(direction, speed);
        }
    }

    private void ApplyMovement(Vector3 direction, float speed)
    {
        _targetRotation = _rotationStrategy.RotationCalculation(
            direction,
            transform,
            ref _smoothRotationVelocity,
            _config.RotationSpeed,
            _targetRotation
        );

        Vector3 moveDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;
        float currentSpeed = speed * direction.magnitude;

        Velocity = new Vector3(moveDirection.x * currentSpeed, _rigidbody.linearVelocity.y, moveDirection.z * currentSpeed);

        _rigidbody.linearVelocity = Velocity;
        Velocity = Velocity;
    }

    public void Stop()
    {
        Velocity = Vector3.zero;
        Velocity = Vector3.zero;

        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
        }
    }

    public void OnPause()
    {
        IsPause = true;
        Stop();
    }

    public void OnResume() => IsPause = false;
}