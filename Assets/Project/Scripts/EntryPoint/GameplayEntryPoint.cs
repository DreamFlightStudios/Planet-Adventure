using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [Inject]
    private void Construct(RootViewUI rootUI, PauseMenuUI pauseMenuUI, WarningIndicator warningIndicatorUI, SubtilesUI subtiles, SceneLoader sceneLoader, InputSystem input)
    {
        pauseMenuUI.Initialize(sceneLoader, input, rootUI);
        warningIndicatorUI.Initialize(input);

        rootUI.ClearSceneUI(); 
        rootUI.AttachSceneUI(warningIndicatorUI.gameObject);
        rootUI.AttachSceneUI(subtiles.gameObject);
        rootUI.AttachSceneUI(pauseMenuUI.gameObject);
    }
}