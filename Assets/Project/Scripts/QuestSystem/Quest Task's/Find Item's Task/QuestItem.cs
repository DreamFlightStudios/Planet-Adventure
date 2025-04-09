public class QuestItem : InteractiveObject
{
    public void Activate(FindItemQuestTask quest)
    {
        CanInteract = true;
        Interacted.AddListener(quest.OnQuestObjectFinded);
    }
}
