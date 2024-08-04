using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform _slotsPanel;
    [SerializeField] private Transform _slots;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _close;
    [SerializeField] private AudioClip _open;

    [Header("DOTweenSettings")]
    [SerializeField] private float _scrollSensivity;
    [SerializeField] private float _scrollDuration;
    [SerializeField] private float _slotsShakingDuration;
    [SerializeField] private float _openInventorySpeed;

    private PlayerInput _playerInput;
    private bool _isOpen;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Start()
    {   
        _slotsPanel.gameObject.SetActive(false);
        _slotsPanel.DOScaleX(0f, 0f);

        _isOpen = _slotsPanel.gameObject.activeSelf;
    }

    private void FixedUpdate()
    {
        Vector2 slotsShakingDiraction = _playerInput.Player.LookInput.ReadValue<Vector2>();
        _slotsPanel.DOLocalMove(slotsShakingDiraction, _slotsShakingDuration);
    }

    private void CloseOpenPanal(InputAction.CallbackContext context)
    {
        _isOpen = !_isOpen;
        _slotsPanel.gameObject.SetActive(_isOpen);

        int finalyScale = _isOpen ? 1 : 0;
        AudioClip clip = _isOpen ? _open : _close;

        _slotsPanel.DOScaleX(finalyScale, _openInventorySpeed);
        _audioManager.PlaySound(clip, SoundType.UI);
    }
    
    private void ScrollSlots(float value)
    {
        if (!_isOpen) return;
        _slots.DOLocalMoveX(_slots.localPosition.x + (_scrollSensivity * value), _scrollDuration);
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