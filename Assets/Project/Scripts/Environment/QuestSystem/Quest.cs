using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Quest : MonoBehaviour
{
    [field: SerializeField] public string Name {  get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [Header("Task's")]
    [SerializeField] private List<QuestTask> _tasks;

    [Header("Event's")]
    [SerializeField] private UnityEvent Activated;
    [SerializeField] private UnityEvent Completed;

    [Header("Setting's")]
    [SerializeField] private bool _activateOnStart;

    private QuestData _data;
    private WarningIndicator _warningIndicator;

    [Inject]
    private void Construct(WarningIndicator warningIndicator) 
        => _warningIndicator = warningIndicator;

    public void Start()
    {
        _data = new QuestData(Activated, Completed);

        foreach (QuestTask task in _tasks)
        {
            task.Initialize();
            task.Activated.AddListener(_warningIndicator.SendTaskInfo);
            task.Completed.AddListener(OnTaskCompleted);
        }

        if (_activateOnStart)
            Activate();
    }

    public void Activate()
    {
        if (_data.IsActive)
            Debug.LogError("The quest is already active");

        _data.CurrentTask = _tasks[0];
        _data.CurrentTask.Activate();

        _data.IsActive = true;
    }

    private void Complete()
    {
        _data.IsComplete = true;
        _data.IsActive = false;
    }

    private void OnTaskCompleted()
    {
        _data.CurrentTask = _tasks[_tasks.IndexOf(_data.CurrentTask) + 1 < _tasks.Count ? _tasks.IndexOf(_data.CurrentTask) + 1 : 0];

        if (_data.CurrentTask == _tasks[0])
        {
            Complete();
            return;
        }

        _data.CurrentTask.Activate();
    }
}