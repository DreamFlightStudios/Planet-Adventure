using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _choiceGameSaveButton;
    [SerializeField] private LaunchLevelsPanel _launchLevelsPanel;
    
    private void Start()
    {
        _launchLevelsPanel.SwitchPanel();
        _choiceGameSaveButton.onClick.AddListener(OnChoiceLevelButtonPressed);
    }

    public void Initialize(SaveLoadController saveLoadController, SceneLoader sceneLoader) 
        => _launchLevelsPanel.Initialize(saveLoadController, sceneLoader);

    public void OnChoiceLevelButtonPressed() 
        => _launchLevelsPanel.SwitchPanel();
}