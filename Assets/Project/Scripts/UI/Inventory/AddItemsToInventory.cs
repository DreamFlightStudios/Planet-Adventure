public readonly struct AddItemsToInventory
{
    public int NotAddedAmount => AddAmount - AddedAmount;

    public readonly string OwnerId;
    public readonly int AddAmount;
    public readonly int AddedAmount;

    public AddItemsToInventory(string ownerId, int addAmount, int addedAmount)
    {
        OwnerId = ownerId;
        AddAmount = addAmount;
        AddedAmount = addedAmount;
    }
}
