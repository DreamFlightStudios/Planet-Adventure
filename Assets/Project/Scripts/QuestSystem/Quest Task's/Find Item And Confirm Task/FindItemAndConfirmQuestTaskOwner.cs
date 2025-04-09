public class FindItemAndConfirmQuestTaskOwner : InteractiveObject
{
    public void OnAllItemsFinded() => CanInteract = true;
}