using UnityEngine;

public class TouchableObject : MonoBehaviour
{
    public bool IsTouched { get; set; }

    [field: SerializeField] public Quaternion HandRotation {  get; private set; }
    [SerializeField] private Collider _touchCollider;

    public Vector3 GetClosestPoint(Vector3 position)
    {
        if (_touchCollider == null) 
            return transform.position;

        return _touchCollider.ClosestPoint(position);
    }
}