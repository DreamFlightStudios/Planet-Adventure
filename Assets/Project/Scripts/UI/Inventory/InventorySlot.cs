using System;

public class InventorySlot
{
    public ItemInfo Item
    {
        get => _data.Item;
        private set => _data.Item = value;
    }

    public event Action<ItemInfo> SlotChanged;
    private readonly InventorySlotData _data = new();

    public void Initialize(InventorySlotView view) => SlotChanged += view.UpdateViewe;

    public void AddItem(ItemInfo item)
    {
        _data.Item = item;
        SlotChanged?.Invoke(Item);
    }
}