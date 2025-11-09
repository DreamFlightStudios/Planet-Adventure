using UnityEngine;

public abstract class AttachableContainerUI : DynamicUI
{
    public void Attach(Transform container) 
        => gameObject.transform.SetParent(container, false);
}