using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hand : MonoBehaviour
{
    public event Action<IInteractive> TargetChanged;

    [SerializeField] private AgentView _view;

    private readonly List<IInteractive> _nearbyObjects = new List<IInteractive>();
    private IInteractive _interactiveObject;
    private InputSystem _input;

    [Inject]
    private void Construct(InputSystem input) => _input = input;

    private void Update() => RefreshTarget();

    private void Interaction(InputAction.CallbackContext context)
    {
        if (_interactiveObject == null)
            return;

        _interactiveObject.Interaction();
        _view.InvokeTrigger(AgentAnimationKey.Interaction);

        RefreshTarget();
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent(out IInteractive interactionObject) == false)
            return;

        if (_nearbyObjects.Contains(interactionObject) == false)
            _nearbyObjects.Add(interactionObject);

        RefreshTarget();
    }

    private void OnTriggerExit(Collider triggerObject)
    {
        if (triggerObject.TryGetComponent(out IInteractive interactionObject))
            _nearbyObjects.Remove(interactionObject);

        RefreshTarget();
    }

    private void RefreshTarget()
    {
        IInteractive nearest = null;
        float nearestDistance = float.MaxValue;

        for (int i = _nearbyObjects.Count - 1; i >= 0; i--)
        {
            IInteractive candidate = _nearbyObjects[i];

            if (IsAlive(candidate) == false)
            {
                _nearbyObjects.RemoveAt(i);
                continue;
            }

            if (candidate.CanInteract == false)
                continue;

            float distance = (candidate.Anchor.position - transform.position).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = candidate;
            }
        }

        if (ReferenceEquals(nearest, _interactiveObject))
            return;

        _interactiveObject = nearest;
        TargetChanged?.Invoke(nearest);
    }

    private bool IsAlive(IInteractive interactive)
        => interactive is Component component && component != null && component.gameObject.activeInHierarchy;

    private void OnEnable() => _input.Player.Interact.performed += Interaction;

    private void OnDisable() => _input.Player.Interact.performed -= Interaction;
}
