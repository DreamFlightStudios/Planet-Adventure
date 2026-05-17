using System.Collections.Generic;
using UnityEngine;

public class StateRestrictionZone : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] private string _defaultState;
    [SerializeField] private List<string> _forbiddenStates = new List<string>();

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

    private void OnDrawGizmos()
    {
        Collider zoneCollider = GetComponent<Collider>();
        if (zoneCollider == null) return;

        Gizmos.color = GetGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;

        Vector3 center = zoneCollider.bounds.center - transform.position;
        Vector3 size = zoneCollider.bounds.size;

        Gizmos.DrawWireCube(center, size);

        Color transparentColor = GetGizmoColor();
        transparentColor.a = 0.3f;
        Gizmos.color = transparentColor;
        Gizmos.DrawCube(center, size);

        Gizmos.matrix = Matrix4x4.identity;
    }

    private Color GetGizmoColor()
    {
        if (_forbiddenStates == null || _forbiddenStates.Count == 0)
            return Color.green;

        return _forbiddenStatesSet?.Count > 0 ? Color.red : Color.yellow;
    }
}