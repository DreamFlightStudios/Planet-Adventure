using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractive
{   
    [SerializeField] private bool _canInteract;
    public bool CanInteract { get => _canInteract; }

    public void Interaction()
    {
        gameObject.SetActive(false);
        _canInteract = false;
    }
}
