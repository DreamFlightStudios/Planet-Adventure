using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AmbientTrigger : AmbientController
{
    private bool _isInside;

    private void OnTriggerEnter(Collider other)
    {
        _isInside = true;
        StartCoroutine(Play());
    }

    private void OnTriggerExit(Collider other)
    {
        _isInside = false;

        _controller.Stop(SourceType.Ambient, _currentAmbient.FadeOutDelay);
        StopCoroutine(Play());
    }

    private void OnDrawGizmosSelected()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(collider.center, collider.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}