using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _checkDistance;

    private void Update()
        => IsGrounded = Physics.CheckSphere(transform.position, _checkDistance, _layers);
}