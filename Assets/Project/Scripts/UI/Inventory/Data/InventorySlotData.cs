using System;

[Serializable]
public class InventorySlotData
{
    public ItemInfo Item
    {
        get => Item;

        private set
        {
            if (value != Item)
            {
                Item = value;
                _itemId = value.Id;
            }
        }
    }

    public int Amount
    {
        get => Amount;

        private set
        {
            if (value != Amount)
            {
                Amount = value;
            }
        }
    }

    public event Action SlotChanged;

    public bool IsEmpty => Amount == 0;
    private string _itemId;

    public void AddItem(ItemInfo item)
    {
        Item = item;

        SlotChanged?.Invoke();
    }

    public void Load(string id)
    {
        _itemId = id;
    }

    public void Save()
    {
        //сохраняет
    }
}
