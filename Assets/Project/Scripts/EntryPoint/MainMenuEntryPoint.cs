using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuControllerUI _mainMenuPrefab;

    [Inject]
    private void Construct(RootControllerUI uiScreens, SaveLoadController saveLoadController, SceneLoader sceneLoader)
    {
        uiScreens.ClearSceneScreens();

        var mainMenu = Instantiate(_mainMenuPrefab);
        mainMenu.Initialize(saveLoadController, sceneLoader);

        uiScreens.Register(ScreenId.MainMenu, mainMenu);
        uiScreens.Show(ScreenId.MainMenu);

        mainMenu.SettingMenuButton.onClick.AddListener(() => uiScreens.Show(ScreenId.Settings));
    }
}
