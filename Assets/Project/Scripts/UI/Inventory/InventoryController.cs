using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _panal;
    [SerializeField] private InventoryProvider _provider;
    private PlayerInput _playerInput;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void CloseOpenPanal(InputAction.CallbackContext context) => _panal.SetActive(!_panal.activeSelf);

    private void OnEnable() => _playerInput.Player.Inventory.performed += CloseOpenPanal;

    private void OnDisable() => _playerInput.Player.Inventory.performed -= CloseOpenPanal;
}