using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuControllerUI : UIScreen
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _pauseMenu;

    public event Action<bool> Paused;
    private SceneLoader _sceneLoader;
    private LevelsConfig _levelsConfig;

    [Inject]
    private void Construct(LevelsConfig levelsConfig)
        => _levelsConfig = levelsConfig;

    private void Awake()
        => _pauseMenu.SetActive(false);

    public void Initialize(SceneLoader sceneLoader, RootControllerUI uiScreens)
    {
        _sceneLoader = sceneLoader;

        _continueButton.onClick.AddListener(() => SwitchState(false));
        _backMenuButton.onClick.AddListener(OnBackMenuButonClicked);
        _settingsButton.onClick.AddListener(() => uiScreens.Show(ScreenId.Settings));
    }

    public override void SwitchState(bool state)
        => SwitchStateByContainer(state, _pauseMenu);

    protected override void OnShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Paused?.Invoke(true);
    }

    protected override void OnHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Paused?.Invoke(false);
    }

    private void OnBackMenuButonClicked()
    {
        _continueButton.onClick.RemoveAllListeners();
        _backMenuButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();

        _sceneLoader.ChangeScene(_levelsConfig.MainMenuSceneName);
    }
}
