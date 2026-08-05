using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private float _checkRadius;

    private void Update() => IsGrounded = Physics.CheckSphere(transform.position, _checkRadius, _groundLayers);
}