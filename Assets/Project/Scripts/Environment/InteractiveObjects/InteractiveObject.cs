using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour, IInteractive
{
    private const int InteractiveObjectLayerIndex = 6;

    [field: SerializeField] public UnityEvent Interacted { get; private set; }
    [field: SerializeField] public string Context { get; private set; }
    [field: SerializeField] public bool CanInteract { get; protected set; }

    [SerializeField] private bool _disableOnInteraction;
    [SerializeField] private bool _multipleInteractions;

    public virtual void Interaction()
    {
        if (_disableOnInteraction)
            gameObject.SetActive(false);

        if (_multipleInteractions == false)
            CanInteract = false;

        Interacted?.Invoke();
    }

    private void Reset()
    {
        if (gameObject.layer != InteractiveObjectLayerIndex)
            gameObject.layer = InteractiveObjectLayerIndex;

        if (TryGetComponent(out Collider collider) == false)
            gameObject.AddComponent<SphereCollider>();
    }
}