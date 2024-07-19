using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryProvider _provider;
    [Header("UI")]
    [SerializeField] private GameObject _panal;
    [SerializeField] private Transform _slotsPanal;
    [SerializeField] private Transform _slots;
    [Header("Settings")]
    [SerializeField] private float _durationScale;
    [SerializeField] private float _durationScroll;
    [SerializeField] private float _scrollSensivity;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void CloseOpenPanal(InputAction.CallbackContext context)
    {
        _panal.SetActive(!_panal.activeSelf);
        if (_panal.activeSelf == true)
        {
            _slotsPanal.DOScaleX(1, 0.1f);
        }
        else
        {
            _slotsPanal.DOScaleX(0, 0.1f);
            //_slotsPanal.localScale = new Vector2(0, _slotsPanal.localScale.y);
        }
    }

    private void ScrollSlots(float value)
    {
        _slots.DOLocalMoveX(_slots.localPosition.x + _scrollSensivity * value, _durationScroll);
    }

    private void OnEnable()
    {
        _playerInput.Player.Inventory.performed += CloseOpenPanal;
        _playerInput.Player.MouseScroll.performed += value => ScrollSlots(value.ReadValue<float>());
    }

    private void OnDisable()
    {
        _playerInput.Player.Inventory.performed -= CloseOpenPanal;
        //_playerInput.Player.MouseScroll.performed -= ScrollSlots;
    }
}