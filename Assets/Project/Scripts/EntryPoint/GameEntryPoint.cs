using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _rootObjects;

    [Inject]
    private void Construct(SceneLoader sceneLoader, LevelsConfig levelsConfig)
    {
        var rootObjects = Instantiate(_rootObjects);
        DontDestroyOnLoad(rootObjects);

        sceneLoader.ChangeScene(levelsConfig.MainMenuSceneName);
    }
}