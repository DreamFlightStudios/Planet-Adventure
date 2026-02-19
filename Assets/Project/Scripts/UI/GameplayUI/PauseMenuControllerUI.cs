using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControllerUI : AttachableContainerUI
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _pauseMenu;

    public event Action<bool> Paused;
    private SceneLoader _sceneLoader;

    private void Awake() 
        => _pauseMenu.SetActive(false);

    public void Initialize(SceneLoader sceneLoader, RootControllerUI rootUI)
    {
        _sceneLoader = sceneLoader;

        _continueButton.onClick.AddListener(() => SwitchState(false));
        _backMenuButton.onClick.AddListener(OnBackMenuButonClicked);
        _settingsButton.onClick.AddListener(rootUI.ShowSettingsMenu);
    }

    public override void SwitchStateByContainer(bool state, GameObject container)
    {
        base.SwitchStateByContainer(state, _pauseMenu);

        Cursor.visible = IsActive;
        Cursor.lockState = IsActive ? CursorLockMode.None : CursorLockMode.Locked;

        Paused?.Invoke(IsActive);
    }

    private void OnBackMenuButonClicked()
    {
        _sceneLoader.ChangeScene("MainMenu");
        Time.timeScale = 1.0f;

        _continueButton.onClick.RemoveAllListeners();
        _backMenuButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }
}