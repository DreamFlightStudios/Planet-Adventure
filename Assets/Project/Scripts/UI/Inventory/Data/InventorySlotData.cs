using System;

[Serializable]
public class InventorySlotData : IStorable
{
    public ItemInfo Item
    {
        get => _data.Item;

        private set
        {
            if (value != _data.Item)
            {
                _data.Item = value;
                _itemId = value.Id;
            }
        }
    }

    public int Amount
    {
        get => _data.Amount;

        private set
        {
            if (value != _data.Amount)
            {
                _data.Amount = value;
            }
        }
    }

    private string _itemId;
    private InventorySlotData _data;
    public event Action ItemChanged;

    public void Load()
    {
        Item = null;
        Amount = 0;

        ItemChanged?.Invoke();
    }

    public void Save()
    {
        //сохраняет
    }
}
