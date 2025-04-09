using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [Inject]
    private void Construct(RootViewUI rootUI, PauseMenuUI pauseMenuUI, WarningIndicator warningIndicatorUI, SceneLoader sceneLoader, InputSystem input)
    {
        pauseMenuUI.Initialize(sceneLoader, input);
        warningIndicatorUI.Initialize(input);

        rootUI.ClearSceneUI();
        rootUI.AttachSceneUI(pauseMenuUI.gameObject);
        rootUI.AttachSceneUI(warningIndicatorUI.gameObject);
    }
}