using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryController))]
public class InventoryProvider : MonoBehaviour
{
    [SerializeField] private InventorySlotViewe[] _viewe;
    [SerializeField] private WarningController _warningController;
    [SerializeField] private string _fullInventoryWarning;

    private List<InventorySlot> _slots = new();

    private void Awake()
    {
        for (int i = 0; i < _viewe.Length; i++)
        {
            var slot = new InventorySlot();
            slot.Initialize(_viewe[i]);
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
                break;
            }
        }
    }
}