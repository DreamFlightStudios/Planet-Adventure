using UnityEngine;

public class PopUpTriggerZone : PopUpCaller
{
    [SerializeField] private bool _showOnce = true;
    [SerializeField] private Color _drawGizmosColor;

    private int _playerCollidersInside;
    private bool _wasShown;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other) == false)
            return;

        _playerCollidersInside++;

        if (_playerCollidersInside > 1 || (_showOnce && _wasShown))
            return;

        _wasShown = true;
        Show();
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
            _playerCollidersInside = Mathf.Max(0, _playerCollidersInside - 1);
    }

    private bool IsPlayer(Collider other)
        => other.GetComponentInParent<PlayerMover>() != null;

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
        if (TryGetComponent(out BoxCollider collider) == false)
            return;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _drawGizmosColor;
        Gizmos.DrawCube(collider.center, collider.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}
