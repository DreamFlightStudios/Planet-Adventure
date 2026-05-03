using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleColliderSizeController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _heightOffset = 0f;
    [SerializeField] private float _radiusOffset = 0f;

    private CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        UpdateColliderSize();
    }

    private void Update()
    {
        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        Bounds bounds = GetMeshBounds();

        if (bounds.size == Vector3.zero) return;

        float height = bounds.size.y + _heightOffset;
        float radius = Mathf.Max(bounds.size.x, bounds.size.z) / 2f + _radiusOffset;

        _capsuleCollider.height = Mathf.Max(height, radius * 2f);
        _capsuleCollider.radius = radius;

        Vector3 center = _capsuleCollider.center;
        center.y = bounds.center.y - transform.position.y;
        _capsuleCollider.center = center;
    }

    private Bounds GetMeshBounds()
    {
        if (_skinnedMeshRenderer != null)
            return _skinnedMeshRenderer.bounds;

        if (_meshRenderer != null)
            return _meshRenderer.bounds;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
            return meshFilter.mesh.bounds;

        return new Bounds(Vector3.zero, Vector3.zero);
    }

    private void OnValidate()
    {
        if (_capsuleCollider == null)
            _capsuleCollider = GetComponent<CapsuleCollider>();

        UpdateColliderSize();
    }
}