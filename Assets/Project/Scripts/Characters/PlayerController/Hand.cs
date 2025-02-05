using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    public event Action<bool, string> ObjectDetected;
    public event Action Interacted;

    private IInteractive _interactionObject;
    private InputSystem _input;

    [Inject]
    private void Construct(InputSystem input) => _input = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if (_interactionObject != null)
        {
            _interactionObject.Interaction();

            if (!_interactionObject.CanInteract)
            {
                _interactionObject = null;
                Interacted?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent(out IInteractive interactionObject))
        {
            if (interactionObject.CanInteract)
            {
                _interactionObject = interactionObject;
                ObjectDetected?.Invoke(true, interactionObject.Context);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_interactionObject == null)
            return;

        ObjectDetected?.Invoke(false, _interactionObject.Context);
        _interactionObject = null;
    }

    private void OnEnable() => _input.Player.Interact.performed += Interaction;

    private void OnDisable() => _input.Player.Interact.performed -= Interaction;
}