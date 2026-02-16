using DG.Tweening;
using UnityEngine;

public class LaunchLevelsPanel : MonoBehaviour
{
    public bool IsOpen {  get; private set; }

    [SerializeField] private LaunchLevelButton[] _launchLevelButtons;
    [SerializeField] private float _switchDuration;
    private SceneLoader _sceneLoader;

    private void Awake() 
        => transform.DOScaleX(0.0f, 0.0f);

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
        int scale = IsOpen ? 0 : 1;
        IsOpen = !IsOpen;

        transform.DOScaleX(scale, _switchDuration);
    }

    private void OnLaunchLevelButtonPressed(string levelId) 
        => _sceneLoader.ChangeScene(levelId);
}