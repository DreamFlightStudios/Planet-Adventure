using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform _slotsPanal;
    [SerializeField] private Transform _slots;
    private Vector2 _slotsShakingDiraction;
    private bool _isInventoryOpen;

    [SerializeField] private float _scrollDuration;
    [SerializeField] private float _scrollSensivity;

    [SerializeField] private InventoryProvider _provider;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void FixedUpdate()
    {
        Vector2 input = _playerInput.Player.LookInput.ReadValue<Vector2>();

        _slotsShakingDiraction = input;
        _slotsPanal.DOLocalMove(_slotsShakingDiraction, 1);
    }

    private void CloseOpenPanal(InputAction.CallbackContext context)
    {
        if (_isInventoryOpen)
        {
            _slotsPanal.DOScaleX(1, 0.1f);
        }
        else
        {
            _slotsPanal.DOScaleX(0, 0.1f);
        }
        _isInventoryOpen = !_isInventoryOpen;
    }

    private void ScrollSlots(float value) => _slots.DOLocalMoveX(_slots.localPosition.x + _scrollSensivity * value, _scrollDuration);

    private void OnEnable()
    {
        _playerInput.Player.Inventory.performed += CloseOpenPanal;
        _playerInput.Player.MouseScroll.performed += value => ScrollSlots(value.ReadValue<float>());
    }

    private void OnDisable()
    {
        _playerInput.Player.Inventory.performed -= CloseOpenPanal;
        _playerInput.Player.MouseScroll.performed -= value => ScrollSlots(value.ReadValue<float>());
    }
}