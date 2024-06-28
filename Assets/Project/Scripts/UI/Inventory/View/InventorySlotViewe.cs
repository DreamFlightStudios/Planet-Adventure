using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewe : MonoBehaviour
{
    public InventorySlotData Data { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private int _ammount;
    [SerializeField] private string _name;

    public void Initialize()
    {
        Data = new InventorySlotData();
        Data.ItemChanged += UpdateViewe;
    }

    public void UpdateViewe()
    {
        var item = Data.Item;
        _icon.sprite = item.Icon;
        _name = item.Name;

        _ammount = Data.Amount;
    }
}
