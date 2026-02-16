using UnityEngine;

public abstract class DynamicUI : Configurable
{
    [field: SerializeField] public bool IsDeactivatedByTrigger { get; private set; }
    [field: SerializeField] public bool IsActivatedByTrigger { get; private set; }
    [field: SerializeField] public int LayerPriority { get; private set; }
    public bool IsActive { get; private set; }

    public virtual void SwitchState(bool state) 
        => SwitchStateByContainer(state, gameObject);

    public virtual void SwitchStateByContainer(bool state, GameObject container)
    {
        container.SetActive(state);
        IsActive = state;
    }
}
