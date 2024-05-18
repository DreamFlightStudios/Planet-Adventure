using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteraction
{
    [SerializeField] private bool _canInteract = true;
    public bool CanInteract() => _canInteract;

    public void Interaction()
    {
        gameObject.SetActive(false);
        _canInteract = false;
    }
}
