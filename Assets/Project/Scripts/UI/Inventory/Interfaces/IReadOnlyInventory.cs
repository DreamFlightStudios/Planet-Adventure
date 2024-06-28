using System;

public interface IReadOnlyInventory
{
    event Action<string, int> ItemsAdded;
    event Action<string, int> ItemsRemoved;

    int GetAmount(string itemId);
    bool Has(string itemId, int amount);
}
