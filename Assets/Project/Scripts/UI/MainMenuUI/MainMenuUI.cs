using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _choiceGameSaveButton;
    [SerializeField] private Button _settingMenuButton;
    [SerializeField] private LaunchLevelsPanel _launchLevelsPanel;

    private SettingsMenuController _settingsMenu;

    public void Initialize(SaveLoadController saveLoadController, SceneLoader sceneLoader, SettingsMenuController settingsMenu)
    {
        _launchLevelsPanel.Initialize(saveLoadController, sceneLoader);
        _settingsMenu = settingsMenu;
    }

    private void Start()
    {
        _choiceGameSaveButton.onClick.AddListener(OnChoiceLevelButtonPressed);
        _settingMenuButton.onClick.AddListener(OnSettingsMenuButtonPressed);
    }

    private void OnChoiceLevelButtonPressed() 
        => _launchLevelsPanel.SwitchPanel();

    private void OnSettingsMenuButtonPressed()
    {
        _settingsMenu.OnShowButtonClicked();

        if (_launchLevelsPanel.IsOpen)
            _launchLevelsPanel.SwitchPanel();
    }
}