using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _checkRadius;

    private void Update()
    {
        Vector3 checkPosition = transform.position;
        IsGrounded = Physics.CheckSphere(checkPosition, _checkRadius, _layers);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 checkPosition = transform.position;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(checkPosition, _checkRadius);
    }
}