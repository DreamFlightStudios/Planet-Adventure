using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractive
{   
    [field: SerializeField] public bool CanInteract { get; private set; }

    public void Interaction()
    {
        gameObject.SetActive(false);
        CanInteract = false;
    }
}
