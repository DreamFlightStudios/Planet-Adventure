using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform _slotsPanal;
    [SerializeField] private Transform _slots;
    private Vector2 _slotsShakingDiraction;
    private bool _isInventoryOpen;

    [SerializeField] private TMP_Text _fullInventoryWarning;

    [SerializeField] private float _scrollDuration;
    [SerializeField] private float _scrollSensivity;

    [SerializeField] private InventoryProvider _provider;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Start()
    {
        _isInventoryOpen = _slotsPanal.gameObject.activeSelf;
    }

    private void FixedUpdate()
    {
        if (!_isInventoryOpen) return;

        Vector2 input = _playerInput.Player.LookInput.ReadValue<Vector2>();
        _slotsShakingDiraction = input;
        _slotsPanal.DOLocalMove(_slotsShakingDiraction, 1);
    }

    private void CloseOpenPanal(InputAction.CallbackContext context)
    {
        _isInventoryOpen = !_isInventoryOpen;

        _slotsPanal.gameObject.SetActive(_isInventoryOpen);
        int finalyScale = _isInventoryOpen ? 1 : 0;

        _slotsPanal.DOScaleX(finalyScale, 0.1f);
    }

    private void ScrollSlots(float value) => _slots.DOLocalMoveX(_slots.localPosition.x + _scrollSensivity * value, _scrollDuration);

    private IEnumerator FullInventoryException()
    {
        _fullInventoryWarning.DOFade(1, 0.1f);

        _fullInventoryWarning.DOFade(0, 0.1f);

        yield return null;

        StopCoroutine(FullInventoryException());
    } 

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