using UnityEngine;
using Zenject;

public class ControlPoint : MonoBehaviour
{
    [SerializeField] private string _nextLevelId;

    private SceneLoader _sceneLoader;
    private SaveLoadController _saveLoadController;

    [Inject]
    private void Construct(SceneLoader sceneLoader, SaveLoadController saveLoadController)
    {
        _sceneLoader = sceneLoader;
        _saveLoadController = saveLoadController;
    }

    public void CompleteLevel()
    {
        _saveLoadController.UnlockGameplayLevel(_nextLevelId);
        _sceneLoader.ChangeScene(_nextLevelId);
    }
}