using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : IReadOnlyInventoryGrid
{
    public Vector2Int Size
    {
        get => _data.Size;
        set
        {
            if(_data.Size != value)
            {
                _data.Size = value;
                SizeChanged?.Invoke(value);
            }
        }
    }

    public event Action<string, int> ItemsAdded;
    public event Action<string, int> ItemsRemoved;
    public event Action<Vector2Int> SizeChanged;

    private readonly InventoryGridData _data;
    private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

    public InventoryGrid(InventoryGridData data)
    {
        _data = data;

        var size = data.Size;
        for (var i = 0; i < size.x; i++)
        {
            for(var j = 0; j < size.y; j++)
            {
                var index = i * size.y + j;
                var slotData = data.Slots[index];
                var slot = new InventorySlot(slotData);
                var position = new Vector2Int(i, j);

                _slotsMap[position]  = slot;
            }
        }
    }

    public IReadOnlyInventorySlot[,] GetSlots()
    {
       var array = new IReadOnlyInventorySlot[Size.x, Size.y];
       
        for(var i  = 0; i < Size.x; i++)
        {
            for (var j = 0;j < Size.y; j++)
            {
                var position = new Vector2Int(i, j);
                array[i, j] = _slotsMap[position];
            }
        }

        return array;
    }

    public AddItemsToInventory AddItems(string itemId, int amount = 1)
    {

    }

    public AddItemsToInventory AddItems(Vector2Int slotCoords, string itemId, int amount = 1)
    {
        var slot = _slotsMap[slotCoords];
        var newValue = slot.Amount + amount;
        var addedItemsAmount = 0;

        if (slot.IsEmpty)
        {
            slot.ItemId = itemId;
        }

        var itemSlotCapacity = GetItemSlotCapacity(itemId);

        if (newValue > itemSlotCapacity)
        {
            var remainingItems = newValue - itemSlotCapacity;
            var addItemsAmount = itemSlotCapacity - slot.Amount;
            addedItemsAmount += addItemsAmount;
            slot.Amount = itemSlotCapacity;

            var result = AddItems(itemId, remainingItems);
            addedItemsAmount += result.AddedAmount;
        }

        else
        {
            addedItemsAmount = amount;
            slot.Amount = newValue;
        }

        return new AddItemsToInventory(OwnerId, amount, addedItemsAmount);
    }

    public RemoveItemsFromInventory RemoveItems(string itemId, int amount = 1)
    {
        var remainingAmount = amount;
        var itemsAddedToSlotsWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

        if (remainingAmount <= 0)
        {
            return new AddItemsToInventory(OwnerId, amount, itemsAddedToSlotsWithSameItemsAmount);
        }

        var itemsAddedToAvilableSlotsAmount = AddToFirstAvailableSlots(itemId, remainingAmount);
        var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvilableSlotsAmount;
        return new AddItemsToInventory(OwnerId, amount, totalAddedItemsAmount);
    }

    private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount)
    {
        var itemAddedAmount = 0;
        remainingAmount = amount;

        for (int i = 0; i< Size.x; i++) 
        {
            for (int j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = _slotsMap[coords];

                if (slot.IsEmpty)
                {
                    continue;
                }

                var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                if (slot.Amount >= slotItemCapacity)
                {
                    continue;
                }

                if (slot.ItemId != itemId)
                {
                    continue;
                }

                var newValue = slot.Amount + remainingAmount;
                if (newValue > slotItemCapacity)
                {
                    remainingAmount = newValue - slotItemCapacity;
                    var addItemsAmount = slotItemCapacity - slot.Amount;
                    itemAddedAmount += addItemsAmount;
                    slot.Amount = slotItemCapacity;

                    if (remainingAmount == 0)
                    {
                        return itemAddedAmount;
                    }
                }
                else
                {
                    itemAddedAmount += remainingAmount;
                    slot.Amount = newValue;
                    remainingAmount = 0;

                    return itemAddedAmount;
                }
            }
        }

        return itemAddedAmount;
    }

    public RemoveItemsFromInventory RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
    {
        var slot = _slotsMap[slotCoords];
        
        if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
        {
            return new RemoveItemsFromInventory(OwnerId, amount, false);
        }

        slot.Amount -= amount;

        if (slot.Amount == 0)
        {
            slot.ItemId = null;
        }

        return new RemoveItemsFromInventory(OwnerId, amount, true);
    }

    public int GetAmount(string itemId)
    {
        throw new NotImplementedException();
    }

    public bool Has(string itemId, int amount)
    {
        throw new NotImplementedException();
    }

    private int GetItemSlotCapacity(string itemId)
    {
        return 99;
    }

}
