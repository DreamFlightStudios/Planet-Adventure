using UnityEngine;

public class FindItemQuestTask : QuestTask
{
    [SerializeField] private QuestItem[] _questItems;

    public override void Activate()
    {
        base.Activate();

        if (Data.IsActive)
        {
            foreach (QuestItem item in _questItems)
                item.Activate(this);
        }
    }

    public virtual void OnQuestObjectFinded()
    {
        if (IsAllItemsFinded())
            Complete();
    }

    protected bool IsAllItemsFinded()
    {
        foreach (QuestItem item in _questItems)
        {
            if (item.CanInteract == true)
                return false;
        }

        return true;
    }
}
