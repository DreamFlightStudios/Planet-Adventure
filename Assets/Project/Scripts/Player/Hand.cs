using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    [SerializeField] private InventoryProvider _inventoryProvider;
    [SerializeField] private AudioManager _audioManager;

    public event Action<bool> ObjectDetected;
    public event Action Interacted;

    private IInteractive _interactionObject;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if (_interactionObject != null)
        {
            if (_interactionObject is PickUpObject pickUpObject && _inventoryProvider.HasEmptySlot())
            {
                _interactionObject.Interaction();
                _inventoryProvider.AddItemToSlot(pickUpObject.Item);
                _audioManager.PlaySound(_interactionObject.InteractionSound, SoundType.Interaction);
                _interactionObject = null;

                Interacted?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent<IInteractive>(out IInteractive interactionObject))
        {
            if (interactionObject.CanInteract)
            {
                _interactionObject = interactionObject;
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