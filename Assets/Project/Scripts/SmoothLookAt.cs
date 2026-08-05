using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;

    [Header("Distance Settings")]
    [SerializeField] private float _distance = 3f;
    [SerializeField] private Vector2 _distanceLimits = new Vector2(1f, 5f);

    [Header("Angle Settings")]
    [SerializeField] private Vector2 _angleLimitsX = new Vector2(-60f, 60f);
    [SerializeField] private Vector2 _angleLimitsY = new Vector2(-45f, 45f);

    [Header("Local Offset")]
    [SerializeField] private Vector3 _localOffset = Vector3.zero;

    [Header("Smoothing")]
    [SerializeField] private float _smoothSpeed = 10f;

    private Vector3 _targetPosition;
    private Vector3 _currentVelocity;

    private void Update()
    {
        Vector3 targetForward = _target.forward;
        Vector3 targetRight = _target.right;
        Vector3 targetUp = _target.up;

        Vector3 direction = targetForward * _distance;
        direction += _target.TransformDirection(_localOffset);

        Vector3 limitedDirection = ApplyAngleLimits(direction, targetRight, targetUp);

        _targetPosition = _target.position + limitedDirection;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            _targetPosition,
            ref _currentVelocity,
            1f / _smoothSpeed
        );
    }

    private Vector3 ApplyAngleLimits(Vector3 direction, Vector3 right, Vector3 up)
    {
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        float horizontalAngle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
        float verticalAngle = Mathf.Asin(localDirection.y / localDirection.magnitude) * Mathf.Rad2Deg;

        horizontalAngle = Mathf.Clamp(horizontalAngle, _angleLimitsY.x, _angleLimitsY.y);
        verticalAngle = Mathf.Clamp(verticalAngle, _angleLimitsX.x, _angleLimitsX.y);

        float distance = direction.magnitude;
        distance = Mathf.Clamp(distance, _distanceLimits.x, _distanceLimits.y);

        Vector3 forward = Quaternion.Euler(0, horizontalAngle, 0) * Vector3.forward;
        Vector3 result = forward * Mathf.Cos(verticalAngle * Mathf.Deg2Rad) * distance;
        result.y = Mathf.Sin(verticalAngle * Mathf.Deg2Rad) * distance;

        return transform.TransformDirection(result);
    }
}