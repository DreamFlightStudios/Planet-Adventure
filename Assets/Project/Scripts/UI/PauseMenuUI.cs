using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private GameObject _pauseMenu;

    private InputSystem _input;

    private void Awake() 
        => _pauseMenu.SetActive(false);

    public void Initialize(SceneLoader sceneLoader, InputSystem input)
    {
        _input = input;
        _input.UI.Pause.performed += Show;
        _backMenuButton.onClick.AddListener(sceneLoader.LoadMainMenu);
    }

    private void Show(InputAction.CallbackContext context)
    {
        bool isPaused = !_pauseMenu.activeSelf;

        _pauseMenu.SetActive(isPaused);
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnDestroy() 
        => _input.UI.Pause.performed -= Show;
}