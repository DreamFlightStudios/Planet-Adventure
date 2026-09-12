using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ControlPoint : MonoBehaviour
{
    [SerializeField] private string _levelId;

    private SceneLoader _sceneLoader;
    private ILevelProgressService _levelProgress;
    private LevelsConfig _levelsConfig;

    [Inject]
    private void Construct(SceneLoader sceneLoader, ILevelProgressService levelProgress, LevelsConfig levelsConfig)
    {
        _sceneLoader = sceneLoader;
        _levelProgress = levelProgress;
        _levelsConfig = levelsConfig;
    }

    public void CompleteLevel()
    {
        string levelId = ResolveLevelId();

        _levelProgress.CompleteLevel(levelId);

        if (_levelProgress.TryGetNextLevelId(levelId, out string nextLevelId))
            _sceneLoader.ChangeScene(nextLevelId);
        else
            _sceneLoader.ChangeScene(_levelsConfig.MainMenuSceneName);
    }

    private string ResolveLevelId()
        => string.IsNullOrEmpty(_levelId) ? SceneManager.GetActiveScene().name : _levelId;
}
