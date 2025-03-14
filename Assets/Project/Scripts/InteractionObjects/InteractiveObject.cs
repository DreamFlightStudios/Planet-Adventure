using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour, IInteractive, IStorable<InteractiveObjectData>
{
    [field: SerializeField] public UnityEvent Interacted { get; private set; }
    [field: SerializeField] public string Context { get; private set; }
    [field: SerializeField] public bool CanInteract { get; protected set; }
    public string Id => gameObject.name.ToString();

    [SerializeField] private bool _disableOnInteraction;
    [SerializeField] private bool _multipleInteractions;
    private InteractiveObjectData _data;

    private void Awake() => _data = new InteractiveObjectData();

    public virtual void Interaction()
    {
        if (_disableOnInteraction)
            gameObject.SetActive(false);

        if (_multipleInteractions == false)
            _data.CanInteract = CanInteract = false;

        Interacted?.Invoke();
    }

    public void SetData(InteractiveObjectData data)
    {
        _data.CanInteract = CanInteract = data.CanInteract;
        _data.IsActive = data.IsActive;
        gameObject.SetActive(data.IsActive);
    }

    public InteractiveObjectData GetData()
    {
        _data.CanInteract = CanInteract;
        _data.IsActive = gameObject.activeSelf;
        return _data;
    }
}