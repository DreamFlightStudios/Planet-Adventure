using UnityEngine;

public class TouchableObject : MonoBehaviour
{
    [SerializeField] private Vector3 _handRotation;
    [SerializeField] private Collider _touchCollider;

    public Quaternion HandRotation => Quaternion.Euler(_handRotation);
    public bool IsTouched { get; set; }

    public Vector3 GetClosestPoint(Vector3 position)
    {
        if (_touchCollider == null) 
            return transform.position;

        return _touchCollider.ClosestPoint(position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        Gizmos.DrawRay(transform.position, HandRotation * Vector3.forward * 0.5f);
    }
}