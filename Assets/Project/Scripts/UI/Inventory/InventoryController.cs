using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour, IAddItemToSlot, IRemoveItemFromSlot
{
    [SerializeField] private InventorySlotViewe[] _viewe;
    private List<InventorySlotData> _slotsData = new();

    private void Awake()
    {
        foreach (var slot in _viewe)
        {
            var data = new InventorySlotData();
            _slotsData.Add(data);
            slot.Initialize(data);
        }

        for (int i = 0; i < _slotsData.Count; i++)
        {

        }
    }

    public bool IsEmptySlots()
    {
        foreach(var slot in _slotsData)
        {
            if (slot.IsEmpty)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(ItemInfo item)
    {
        foreach (var slot in _slotsData)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
            }
        }
    }

    public void RemoveItem(InventorySlotData slot) => slot.RemoveItem();
}
