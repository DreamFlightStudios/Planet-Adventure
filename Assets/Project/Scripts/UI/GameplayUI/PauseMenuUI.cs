using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _pauseMenu;

    private SceneLoader _sceneLoader;
    private InputSystem _input;
    private bool _isPaused;

    private void Awake() 
        => _pauseMenu.SetActive(false);

    public void Initialize(SceneLoader sceneLoader, InputSystem input, RootViewUI rootViewUI)
    {
        _input = input;
        _sceneLoader = sceneLoader;

        _continueButton.onClick.AddListener(Show);
        _backMenuButton.onClick.AddListener(OnBackMenuButonClicked);
        _settingsButton.onClick.AddListener(rootViewUI.ShowSettingsMenu);

        _input.UI.Pause.performed += OnPausePerformed;
    }

    private void Show()
    {
        _isPaused = !_pauseMenu.activeSelf;

        _pauseMenu.SetActive(_isPaused);
        Cursor.visible = _isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (_isPaused)
            return;

        Show();
    }

    private void OnBackMenuButonClicked()
    {
        _sceneLoader.ChangeScene("MainMenu");

        _continueButton.onClick.RemoveAllListeners();
        _backMenuButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();

        _input.UI.Pause.performed -= OnPausePerformed;
    }
}