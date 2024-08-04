using System.Collections.Generic;
using UnityEngine;

public class InventoryProvider : MonoBehaviour
{
    [SerializeField] private InventorySlotView[] _view;
    private List<InventorySlot> _slots = new();

    [SerializeField] private WarningController _warningController;
    [SerializeField] private string _fullInventoryWarning;
    [SerializeField] private string _newItemWarning;

    private void Awake()
    {
        foreach (var view in _view)
        {
            var slot = new InventorySlot();
            slot.Initialize(view);
            _slots.Add(slot);
        }
    }

    public bool HasEmptySlot()
    {
        foreach (var slot in _slots)
        {
            if (!slot.Item)
            {
                return true;
            }
        }
        _warningController.InvokeWarning(_fullInventoryWarning, WarningType.FullInventory);
        return false;
    }

    public void AddItemToSlot(ItemInfo item)
    {
        foreach (var slot in _slots)
        {
            if (!slot.Item)
            {
                slot.AddItem(item);
                _warningController.InvokeWarning(_newItemWarning + item.Name, WarningType.NewItem);
                break;
            }
        }
    }
}