using UnityEngine;
using UnityEngine.Events;

public abstract class QuestTask : MonoBehaviour
{
    [field: SerializeField] public string Description { get; private set; }

    [Header("Event's")]
    [SerializeField] public UnityEvent<string> Activated;
    [SerializeField] public UnityEvent Completed;

    [SerializeField] private bool _selfActivation = true;

    protected QuestTaskData Data;

    public void Initialize() 
        => Data = new(Activated, Completed, Description);

    public virtual void Activate()
    {
        if (Data.IsComplete)
            Debug.LogError("The task is already active");

        if (_selfActivation)
            Data.IsActive = true;
    }

    protected virtual void Complete()
    {
        Data.IsComplete = true;
        Data.IsActive = false;
    }
}