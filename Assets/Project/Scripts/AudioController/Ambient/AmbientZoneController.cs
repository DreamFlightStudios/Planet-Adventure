using UnityEngine;

public class AmbientZoneController : AmbientController
{
    [SerializeField] private Color _drawGizmosColor;

    private void OnTriggerEnter(Collider other) 
        => StartPlaying();

    private void OnTriggerExit(Collider other) 
        => StopPlaying();

    private void Reset()
    {
        if (TryGetComponent(out BoxCollider collider) == false)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;

            int playerLayer = LayerMask.NameToLayer("Player");
            LayerMask playerLayerMask = 1 << playerLayer;

            collider.includeLayers = playerLayerMask;
            collider.excludeLayers = ~playerLayerMask;
        }
    }

    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _drawGizmosColor;
        Gizmos.DrawCube(collider.center, collider.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}