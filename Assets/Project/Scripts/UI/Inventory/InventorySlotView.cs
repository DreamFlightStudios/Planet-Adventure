using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class InventorySlotView
{
    private readonly Image _icon;
    private readonly TMP_Text _name;
    private readonly float _fadeInIconSpeed;

    public InventorySlotView(Image icon, TMP_Text name, float fadeInIconSpeed, InventorySlotHandler handler)
    {
        _icon = icon;
        _name = name;
        _fadeInIconSpeed = fadeInIconSpeed;

        handler.SlotChanged += UpdateViewe;
    }

    private void UpdateViewe(ItemInfo item)
    {
        if (item == null)
            _icon.DOFade(0, 0);
        else 
            _icon.DOFade(1, _fadeInIconSpeed);

        _icon.sprite = item.Icon;
        _name.text = item.Name;
    }
}