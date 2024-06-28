using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventorySlotViewe[] _slots;

    private void Awake()
    {
        foreach (var slot in _slots)
        {
            slot.Initialize();
        }
    }

    private void UpdateSlots()
    {
        
    }

    public void RemoveItem()
    {

    }

    public void AddItem(ItemInfo item)
    {
        //foreach (var slot in _slots)
        //{
        //    if (slot.Data.Item == null)
        //    {
        //        slot.Data.
        //    }
        //}
    }

    private void OnEnable() => UpdateSlots();

    private void OnDisable()
    {
        
    }
}
