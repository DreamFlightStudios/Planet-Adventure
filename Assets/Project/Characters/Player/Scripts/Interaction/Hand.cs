using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    private IInteraction _interactionObject;
    private PlayerInput _playerInput;
    private bool _canInteract;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if(_interactionObject != null && _canInteract)
        {
            _interactionObject.Interaction();
            AddItem();
        }
    }

    private void AddItem()
    {
        //add item
    }

    private void OnTrigger(bool onEnter, GameObject triggerObject)
    {
        IInteraction interactionObject = triggerObject?.GetComponent<IInteraction>();
        _canInteract = onEnter;
        if (onEnter && interactionObject != null)
        {
            _interactionObject = interactionObject;
            return;
        }
        _interactionObject = null;
    }

    private void OnTriggerEnter(Collider other) => OnTrigger(true, other.gameObject);

    private void OnTriggerExit(Collider other) => OnTrigger(false, other.gameObject);

    private void OnEnable() => _playerInput.Player.Interaction.performed += Interaction;

    private void OnDisable() => _playerInput.Player.Interaction.performed -= Interaction;
}