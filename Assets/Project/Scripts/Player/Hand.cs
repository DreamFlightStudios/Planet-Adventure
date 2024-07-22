using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    [SerializeField] private InventoryProvider _inventoryProvider;

    public event Action<bool> ObjectDetected;
    public event Action Interacted;

    private IInteractive _interactionObject;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if(_interactionObject != null)
        {
            if (_interactionObject is PickUpObject pickUpObject && _inventoryProvider.HasEmptySlot())
            {
                _inventoryProvider.AddItemToSlot(pickUpObject.Item);
                _interactionObject.Interaction();
                _interactionObject = null;
                Interacted?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent<IInteractive>(out IInteractive interactionObject))
        {
            _interactionObject = interactionObject;
            if(_interactionObject.CanInteract)
            {
                ObjectDetected(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _interactionObject = null;
        ObjectDetected(false);
    }

    private void OnEnable() => _playerInput.Player.Interaction.performed += Interaction;

    private void OnDisable() => _playerInput.Player.Interaction.performed -= Interaction;
}