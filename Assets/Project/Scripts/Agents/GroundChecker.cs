using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _extraDistance;

    [SerializeField] private CapsuleCollider _collider;

    private void Update()
    {
        Vector3 checkPosition = GetBottomPosition();
        IsGrounded = Physics.CheckSphere(checkPosition, _checkRadius, _groundLayers);
    }

    private Vector3 GetBottomPosition()
    {
        Vector3 center = transform.position + _collider.center;
        float bottomY = center.y - _collider.height / 2f + _collider.radius;

        return new Vector3(center.x, bottomY - _extraDistance, center.z);
    }
}