using UnityEngine.Events;

public class QuestData
{
    private readonly UnityEvent Activated;
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
                Activated?.Invoke();
        }
    }

    public QuestTask CurrentTask;

    private bool _isComplete;
    private bool _isActive;

    public QuestData(UnityEvent activated, UnityEvent completed)
    {
        Activated = activated;
        Completed = completed;
    }
}