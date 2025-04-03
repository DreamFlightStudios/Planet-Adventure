using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private PauseMenuUI _pauseMenuPrefab;
    [SerializeField] private StorableContainer _storableContainer;

    [Inject]
    private void Construct(RootViewUI rootUI, SceneLoader sceneLoader, InputSystem input)
    {
        sceneLoader.Initialize(_storableContainer);

        var gameplayUI = Instantiate(_pauseMenuPrefab);
        gameplayUI.Initialize(sceneLoader, input);

        rootUI.AttachSceneUI(gameplayUI.gameObject);
    }
}