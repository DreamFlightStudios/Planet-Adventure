using System;

[Serializable]
public class InventorySlotData : IAddItemToSlot, IRemoveItemFromSlot
{
    public ItemInfo Item { get; private set; }
    public string _itemId { get; private set; }

    public bool IsEmpty => Item == null;

    public event Action SlotChanged;

    public void AddItem(ItemInfo item)
    {
        Item = item;
        SlotChanged?.Invoke();
    }

    public void RemoveItem()
    {
        Item = null; 
        SlotChanged?.Invoke();
    }
}
