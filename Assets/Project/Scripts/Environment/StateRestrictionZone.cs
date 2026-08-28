using System.Collections.Generic;
using UnityEngine;

public class StateRestrictionZone : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] private string _defaultState;
    [SerializeField] private List<string> _forbiddenStates = new List<string>();
    [SerializeField] private Color _drawGizmosColor;

    private HashSet<string> _forbiddenStatesSet;
    private HashSet<IAgentStateMachine> _agentsInside = new HashSet<IAgentStateMachine>();

    private void Awake() => _forbiddenStatesSet = new HashSet<string>(_forbiddenStates);

    private void OnTriggerEnter(Collider other)
    {
        IAgentStateMachine stateMachine = other.GetComponent<IAgentStateMachine>();

        if (stateMachine == null) 
            return;

        _agentsInside.Add(stateMachine);
        ApplyStateRestrictions(stateMachine, true);
    }

    private void OnTriggerExit(Collider other)
    {
        IAgentStateMachine stateMachine = other.GetComponent<IAgentStateMachine>();

        if (stateMachine == null) 
            return;

        _agentsInside.Remove(stateMachine);
        ApplyStateRestrictions(stateMachine, false);
    }

    private void ApplyStateRestrictions(IAgentStateMachine stateMachine, bool isEntering)
    {
        var restrictions = new HashSet<string>(_forbiddenStatesSet);
        stateMachine.SetStatesRestrictions(_defaultState, restrictions, isEntering);
    }

    private void Reset()
    {
        if (TryGetComponent(out BoxCollider collider) == false)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;

            int playerLayer = LayerMask.NameToLayer("Player");
            LayerMask playerLayerMask = 1 << playerLayer;

            collider.includeLayers = playerLayerMask;
            collider.excludeLayers = ~playerLayerMask;
        }
    }

    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _drawGizmosColor;
        Gizmos.DrawCube(collider.center, collider.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(collider.center, collider.size);
    }
}