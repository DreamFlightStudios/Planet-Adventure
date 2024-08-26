using UnityEngine;

public class InventoryProvider : MonoBehaviour
{
    [SerializeField] private InventorySlotHandler[] _slots;

    [SerializeField] private WarningController _warningController;
    [SerializeField] private string _fullInventoryWarning;
    [SerializeField] private string _newItemWarning;

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