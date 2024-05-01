using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Vector2 _cameraDistanceMinMax;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private Transform _camera;

    private Vector3 _cameraDirection;
    private float _cameraDistance;
   
    private void Start()
    {
        _cameraDirection = _camera.localPosition.normalized;
        _cameraDistance = _cameraDistanceMinMax.y;
    }

    private void FixedUpdate()
    {
        CheckCameraCollision();
        
        _camera.localPosition = _cameraDirection * _cameraDistance;
    }

    private void CheckCameraCollision()
    {
        Vector3 directedCameraPosition = transform.TransformPoint(_cameraDirection * _cameraDistanceMinMax.y);

        RaycastHit hit;
        if(Physics.Linecast(transform.position, directedCameraPosition, out hit, _collisionLayers))
        {
            _cameraDistance = Mathf.Clamp(hit.distance, _cameraDistanceMinMax.x, _cameraDistanceMinMax.y);
        }
        else _cameraDistance = _cameraDistanceMinMax.y;
    }
}
