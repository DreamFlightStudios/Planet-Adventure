using System;

public class InventorySlot
{
    public ItemInfo Item
    {
        get
        {
            return _data.Item;
        }
        private set
        {
            if (_data.Item != value)
                _data.Item = value;
        }
    }

    public event Action<ItemInfo> SlotChanged;

    private readonly InventorySlotData _data = new();
    private InventorySlotViewe _view;

    public void Initialize(InventorySlotViewe view)
    {
        _view = view;
        SlotChanged += view.UpdateViewe;
    }

    public void AddItem(ItemInfo item)
    {
        _data.Item = item;
        SlotChanged?.Invoke(Item);
    }

    public void RemoveItem()
    {
        _data.Item = null;
        SlotChanged?.Invoke(Item);
    }
}