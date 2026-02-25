using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AmbientZoneController : AmbientController
{
    [SerializeField] private Color _drawGizmosColor;

    private void OnTriggerEnter(Collider other) 
        => StartPlaying();

    private void OnTriggerExit(Collider other) 
        => StopPlaying();

    private void OnDrawGizmosSelected()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _drawGizmosColor;
        Gizmos.DrawCube(collider.center, collider.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}