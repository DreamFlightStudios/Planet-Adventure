using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotHandler : MonoBehaviour
{
    public ItemInfo Item
    {
        get => _data.Item;
        private set => _data.Item = value;
    }

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private float _fadeInIconSpeed;

    private InventorySlotData _data;
    private InventorySlotView _view;

    public event Action<ItemInfo> SlotChanged;

    private void Awake()
    {
        _data = new();
        _view = new(_icon, _name, _fadeInIconSpeed, this);
    }

    public void AddItem(ItemInfo item)
    {
        _data.Item = item;
        SlotChanged?.Invoke(Item);
    }
}