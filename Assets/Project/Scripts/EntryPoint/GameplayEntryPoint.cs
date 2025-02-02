using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private PauseMenuUI _pauseMenuPrefab;

    [Inject]
    private void Construct(RootViewUI rootUI, SceneLoader sceneLoader, InputSystem input)
    {
        var gameplayUI = Instantiate(_pauseMenuPrefab);
        gameplayUI.Initialize(sceneLoader, input);

        rootUI.AttachSceneUI(gameplayUI.gameObject);
    }
}