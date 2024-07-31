using DG.Tweening;
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

    [Header("Audio")]
    [SerializeField] private TMP_Text _fullInventoryWarning;
    [SerializeField] AudioManager _audioManager;
    [SerializeField] private AudioClip _close;
    [SerializeField] private AudioClip _open;

    [SerializeField] private float _scrollDuration;
    [SerializeField] private float _scrollSensivity;

    private PlayerInput _playerInput;

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
        AudioClip clip = _isInventoryOpen ? _open : _close;

        _slotsPanal.DOScaleX(finalyScale, 0.1f);
        _audioManager.PlaySound(clip, SoundType.UI);
    }

    private void ScrollSlots(float value) => _slots.DOLocalMoveX(_slots.localPosition.x + (_scrollSensivity * value), _scrollDuration);

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