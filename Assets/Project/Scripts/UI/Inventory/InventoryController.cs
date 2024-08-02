using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Transform _slotsPanal;
    [SerializeField] private Transform _slots;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _close;
    [SerializeField] private AudioClip _open;
    [SerializeField] private TMP_Text _fullInventoryWarning;

    [Header("DOTweenSettings")]
    [SerializeField] private float _scrollSensivity;
    [SerializeField] private float _scrollDuration;
    [SerializeField] private float _slotsShakingDuration;
    [SerializeField] private float _openInventorySpeed;

    private PlayerInput _playerInput;
    private bool _isInventoryOpen;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Start()
    {   
        _slotsPanal.gameObject.SetActive(false);
        _slotsPanal.DOScaleX(0f, 0f);

        _isInventoryOpen = _slotsPanal.gameObject.activeSelf;
    }

    private void FixedUpdate()
    {
        Vector2 slotsShakingDiraction = _playerInput.Player.LookInput.ReadValue<Vector2>();
        _slotsPanal.DOLocalMove(slotsShakingDiraction, _slotsShakingDuration);
    }

    private void CloseOpenPanal(InputAction.CallbackContext context)
    {
        _isInventoryOpen = !_isInventoryOpen;
        _slotsPanal.gameObject.SetActive(_isInventoryOpen);

        int finalyScale = _isInventoryOpen ? 1 : 0;
        AudioClip clip = _isInventoryOpen ? _open : _close;

        _slotsPanal.DOScaleX(finalyScale, _openInventorySpeed);
        _audioManager.PlaySound(clip, SoundType.UI);
    }
    
    private void ScrollSlots(float value)
    {
        if (!_isInventoryOpen) return;
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