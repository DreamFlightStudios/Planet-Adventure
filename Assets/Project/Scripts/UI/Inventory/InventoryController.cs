using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
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
            //_slotsData[i].Load(_stateProvider.xtnj[i]);
        }
    }

    public void RemoveItem(ItemInfo item)
    {
     
    }

    public void AddItem(ItemInfo item)
    {
    
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }
}
