using UnityEngine;

public abstract class UIScreen : Configurable
{
    [field: SerializeField] public UILayer Layer { get; private set; }
    [field: SerializeField] public bool IsModal { get; private set; }
    [field: SerializeField] public bool IsActivatedByTrigger { get; private set; }
    [field: SerializeField] public bool IsDeactivatedByTrigger { get; private set; }
    [field: SerializeField] public int LayerPriority { get; private set; }

    public bool IsActive { get; protected set; }
    public bool IsCreated { get; private set; }

    public void Attach(Transform container)
        => transform.SetParent(container, false);

    public void CreateScreen()
    {
        if (IsCreated)
            return;

        IsCreated = true;
        OnCreate();
    }

    public void DisposeScreen()
    {
        OnDispose();
        IsCreated = false;
    }

    public virtual void SwitchState(bool state)
        => SwitchStateByContainer(state, gameObject);

    public virtual void SwitchStateByContainer(bool state, GameObject container)
    {
        container.SetActive(state);
        ApplyState(state);
    }

    protected void ApplyState(bool state)
    {
        IsActive = state;

        if (state)
            OnShow();
        else
            OnHide();
    }

    protected virtual void OnCreate() { }
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
    protected virtual void OnDispose() { }
}
