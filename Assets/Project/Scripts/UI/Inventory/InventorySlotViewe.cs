using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewe : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;

    public void UpdateViewe(ItemInfo item)
    {
        _icon.sprite = item.Icon;
        _name.text = item.Name;
    }
}