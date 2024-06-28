public struct RemoveItemsFromInventory
{
    public readonly string OwnerId;
    public readonly int RemoveAmount;
    public readonly bool Success;

    public RemoveItemsFromInventory(string ownerId, int removeAmount, bool success)
    {
        OwnerId = ownerId;
        RemoveAmount = removeAmount;
        Success = success;
    }
}
