using UnityEngine;
using UnityEngine.UI;

public class MainMenuControllerUI : UIScreen
{
    [field: SerializeField] public Button SettingMenuButton { get; private set; }

    [SerializeField] private Button _choiceGameSaveButton;
    [SerializeField] private LaunchLevelsPanel _launchLevelsPanel;

    private void Start()
    {
        _choiceGameSaveButton.onClick.AddListener(OnChoiceLevelButtonPressed);
        SettingMenuButton.onClick.AddListener(OnSettingsMenuButtonPressed);
    }

    private void OnChoiceLevelButtonPressed() 
        => _launchLevelsPanel.SwitchPanel();

    private void OnSettingsMenuButtonPressed()
    {
        if (_launchLevelsPanel.IsOpen)
            _launchLevelsPanel.SwitchPanel();
    }

    private void OnDestroy()
    {
        SettingMenuButton.onClick.RemoveAllListeners();
        _choiceGameSaveButton.onClick.RemoveAllListeners();
    }
}