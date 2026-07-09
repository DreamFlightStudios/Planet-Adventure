using UnityEngine;

public class PlayerRotationStrategy : AgentRoatationStrategy
{
    [Header("Transforms")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _playerTransform;

    [Header("Parameters")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationLag;
    [SerializeField] private float _wallCheckRadius;
    [SerializeField] private float _wallCheckHeight;
    [SerializeField] private LayerMask _wallLayers;

    private float _currentRotation;
    private float _rotationVelocity;
    private Vector2 _lastInput;

    protected override float RotationStrategy(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            _lastInput = Vector2.zero;
            return _currentRotation;
        }

        float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;

        if (IsInstantTurn(input))
        {
            _currentRotation = targetAngle;
            _rotationVelocity = 0;
        }
        else if (IsWallNearby())
        {
            _currentRotation = targetAngle;
            _rotationVelocity = 0;
        }
        else
        {
            _currentRotation = SmoothTurn(targetAngle, input.magnitude);
        }

        _lastInput = input;
        return _currentRotation;
    }

    private bool IsInstantTurn(Vector2 input)
    {
        if (_lastInput == Vector2.zero) return false;

        float dot = Vector2.Dot(_lastInput.normalized, input.normalized);
        return dot < -0.3f;
    }

    private bool IsWallNearby()
    {
        if (_playerTransform == null) return false;

        Vector3 spherePosition = _playerTransform.position + Vector3.up * _wallCheckHeight;
        Collider[] colliders = Physics.OverlapSphere(spherePosition, _wallCheckRadius, _wallLayers);

        return colliders.Length > 0;
    }

    private float SmoothTurn(float targetAngle, float magnitude)
    {
        float delta = Mathf.DeltaAngle(_currentRotation, targetAngle);
        float maxDelta = _rotationSpeed * magnitude * Time.deltaTime;
        float step = Mathf.Clamp(delta, -maxDelta, maxDelta);

        return Mathf.SmoothDampAngle(_currentRotation, _currentRotation + step, ref _rotationVelocity, _rotationLag);
    }
}