using UnityEngine;

public class FindItemAndConfirmQuestTask : FindItemQuestTask
{
    [SerializeField] FindItemAndConfirmQuestTaskOwner _owner;

    public override void Activate()
    {
        base.Activate();
        _owner.Interacted.AddListener(Complete);
    }

    public override void OnQuestObjectFinded()
    {
        if (IsAllItemsFinded())
            _owner.OnAllItemsFinded();
    }
}