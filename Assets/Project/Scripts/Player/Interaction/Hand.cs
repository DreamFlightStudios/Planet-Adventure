using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    private IInteractive _interactionObject;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if(_interactionObject != null && _interactionObject.CanInteract)
        {
            _interactionObject.Interaction();
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent<IInteractive>(out IInteractive interactionObject))
        {
            _interactionObject = interactionObject;
        }
    }

    private void OnTriggerExit(Collider other) => _interactionObject = null;

    private void OnEnable() => _playerInput.Player.Interaction.performed += Interaction;

    private void OnDisable() => _playerInput.Player.Interaction.performed -= Interaction;
}