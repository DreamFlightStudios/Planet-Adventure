using UnityEngine;
using UnityEngine.Events;

public class QuestTaskData : MonoBehaviour
{
    private readonly string Description;

    private readonly UnityEvent<string> Activated;
    private readonly UnityEvent Completed;

    public bool IsComplete
    {
        get => _isComplete;
        set
        {
            if (value == _isComplete)
                return;

            _isComplete = value;

            if (_isComplete == true)
                Completed?.Invoke();
        }
    }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value == _isActive)
                return;

            _isActive = value;

            if (_isActive == true)
                Activated?.Invoke(Description);
        }
    }

    private bool _isComplete;
    private bool _isActive;

    public QuestTaskData(UnityEvent<string> activated, UnityEvent completed, string description)
    {
        Activated = activated;
        Completed = completed;
        Description = description;
    }
}