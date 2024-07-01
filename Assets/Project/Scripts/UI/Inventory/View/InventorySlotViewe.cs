using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewe : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _ammount;
    [SerializeField] private TMP_Text _name;
    
    private InventorySlotData _data;

    public void Initialize(InventorySlotData data)
    {
        _data = data;

        _data.SlotChanged += UpdateViewe;
        UpdateViewe();
    }

    private void UpdateViewe()
    {
        _icon.sprite = _data.Item.Icon;
        _name.text = _data.Item.Name;
        _ammount.text = _data.Amount.ToString();
    }

    private void OnEnable() => UpdateViewe();

    private void OnDisable() => UpdateViewe();
}
