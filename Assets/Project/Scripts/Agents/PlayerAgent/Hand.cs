using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    public event Action<bool, string> ObjectDetected;
    public event Action Interacted;

    [SerializeField] private AgentView _view;
    private IInteractive _interactiveObject;
    private InputSystem _input;

    [Inject]
    private void Construct(InputSystem input) => _input = input;

    private void Interaction(InputAction.CallbackContext context)
    {
        if (_interactiveObject != null)
        {
            _interactiveObject.Interaction();
            _view.InvokeTrigger(AgentAnimationKey.Interaction);

            if (!_interactiveObject.CanInteract)
            {
                _interactiveObject = null;
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
                _interactiveObject = interactionObject;
                ObjectDetected?.Invoke(true, interactionObject.Context);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_interactiveObject == null)
            return;

        ObjectDetected?.Invoke(false, _interactiveObject.Context);
        _interactiveObject = null;
    }

    private void OnEnable() => _input.Player.Interact.performed += Interaction;

    private void OnDisable() => _input.Player.Interact.performed -= Interaction;
}