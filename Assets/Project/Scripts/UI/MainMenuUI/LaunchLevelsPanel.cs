using DG.Tweening;
using UnityEngine;

public class LaunchLevelsPanel : MonoBehaviour
{
    [SerializeField] private LaunchLevelButton[] _launchLevelButtons;
    [SerializeField] private float _switchDuration;
    [SerializeField] private bool _isOpen;
    private SceneLoader _sceneLoader;

    public void Initialize(SaveLoadController saveLoadController, SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        var levelsData = saveLoadController.UserData.LevelsData;

        for (int i = 0; i < _launchLevelButtons.Length; i++)
        {
            var button = _launchLevelButtons[i];
            button.ButtonPressed += OnLaunchLevelButtonPressed;

            if (!levelsData.ContainsKey(button.LevelId))
            {
                button.Initialize();
                saveLoadController.InitializeGameplayLevel(button.LevelData);
            }

            button.Initialize(i == 0 ? true : levelsData[button.LevelId]);
        }
    }

    public void SwitchPanel()
    {
        int scale = _isOpen ? 0 : 1;
        _isOpen = !_isOpen;

        transform.DOScaleX(scale, _switchDuration);
    }

    private void OnLaunchLevelButtonPressed(string levelId) 
        => _sceneLoader.ChangeScene(levelId);
}