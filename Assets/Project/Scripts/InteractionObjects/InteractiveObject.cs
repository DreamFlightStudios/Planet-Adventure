using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class InteractiveObject : MonoBehaviour, IInteractive
{
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
}