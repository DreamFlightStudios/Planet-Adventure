using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [Inject]
    private void Construct(SaveLoadController saveLoadController, RootControllerUI rootUI, PauseMenuControllerUI pauseMenuUI, WarningIndicator warningIndicatorUI, SubtilesUI subtiles, SceneLoader sceneLoader, InputSystem input)
    {
        pauseMenuUI.Initialize(sceneLoader, rootUI);
        warningIndicatorUI.Initialize(input);

        rootUI.ClearSceneUI(); 
        rootUI.AttachSceneUI(warningIndicatorUI);
        rootUI.AttachSceneUI(subtiles);
        rootUI.AttachSceneUI(pauseMenuUI);

        saveLoadController.UpdateUserData();
    }
}