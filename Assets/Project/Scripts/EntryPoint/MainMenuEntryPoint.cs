using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuControllerUI _mainMenuPrefab;

    [Inject]
    private void Construct(DiContainer container, RootControllerUI uiScreens)
    {
        uiScreens.ClearSceneScreens();

        var mainMenu = container.InstantiatePrefabForComponent<MainMenuControllerUI>(_mainMenuPrefab);

        uiScreens.Register(ScreenId.MainMenu, mainMenu);
        uiScreens.Show(ScreenId.MainMenu);

        mainMenu.SettingMenuButton.onClick.AddListener(() => uiScreens.Show(ScreenId.Settings));
    }
}
