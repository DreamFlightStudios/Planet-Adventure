using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleColliderSizeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _hipBone;
    [SerializeField] private Transform _headBone;
    [SerializeField] private Transform _leftLegBone;
    [SerializeField] private Transform _rightLegBone;

    [Header("Settings")]
    [SerializeField] private float _extraHeight = 0.1f;
    [SerializeField] private float _extraRadius = 0.05f;
    [SerializeField] private bool _updateEveryFrame = true;

    private CapsuleCollider _capsuleCollider;

    private void Awake() 
        => _capsuleCollider = GetComponent<CapsuleCollider>();

    private void Start() 
        => UpdateColliderSize();

    private void Update()
    {
        if (_updateEveryFrame)
        UpdateColliderSize();
    }

    [ContextMenu("Update Collider Size")]
    private void UpdateColliderSize()
    {
        if (_capsuleCollider == null) return;
        if (_hipBone == null || _headBone == null) return;

        Vector3 bottom = GetBottomPosition();
        Vector3 top = _headBone.position;

        float height = Vector3.Distance(bottom, top) + _extraHeight;
        float radius = GetAverageLegRadius() + _extraRadius;

        _capsuleCollider.height = height;
        _capsuleCollider.radius = radius;

        Vector3 center = (bottom + top) / 2f;
        _capsuleCollider.center = transform.InverseTransformPoint(center);
    }

    private Vector3 GetBottomPosition()
    {
        Vector3 bottom = _hipBone.position;

        if (_leftLegBone != null && _rightLegBone != null)
        {
            bottom = (_leftLegBone.position + _rightLegBone.position) / 2f;
        }

        return bottom;
    }

    private float GetAverageLegRadius()
    {
        if (_leftLegBone != null && _rightLegBone != null)
        {
            float leftRadius = GetHorizontalRadius(_leftLegBone);
            float rightRadius = GetHorizontalRadius(_rightLegBone);
            return (leftRadius + rightRadius) / 2f;
        }

        return 0.3f;
    }

    private float GetHorizontalRadius(Transform bone)
    {
        Vector3 right = bone.TransformDirection(Vector3.right);
        Vector3 left = bone.TransformDirection(Vector3.left);

        return 0.25f;
    }
}