using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewe : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private float _fadeInIconSpeed;

    public void UpdateViewe(ItemInfo item)
    {
        if (item == null)
            _icon.DOFade(0, 0);
        else 
            _icon.DOFade(1, _fadeInIconSpeed);

        _icon.sprite = item.Icon;
        _name.text = item.Name;
    }
}