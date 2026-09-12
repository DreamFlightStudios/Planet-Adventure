using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    [Inject]
    private void Construct(SaveLoadController saveLoadController, RootControllerUI uiScreens, PauseMenuControllerUI pauseMenuUI, WarningIndicator warningIndicatorUI, SubtilesUI subtiles, SceneLoader sceneLoader, InputSystem input)
    {
        uiScreens.ClearSceneScreens();

        pauseMenuUI.Initialize(sceneLoader, uiScreens);
        warningIndicatorUI.Initialize(input);

        uiScreens.Register(ScreenId.Pause, pauseMenuUI);
        uiScreens.Register(ScreenId.WarningIndicator, warningIndicatorUI);
        uiScreens.Register(ScreenId.Subtitles, subtiles);

        uiScreens.Show(ScreenId.WarningIndicator);
        uiScreens.Show(ScreenId.Subtitles);

        saveLoadController.UpdateUserData();
    }
}
