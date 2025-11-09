using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuPrefab;

    [Inject]
    private void Construct(RootControllerUI rootUI, SaveLoadController saveLoadController, SceneLoader sceneLoader)
    {
        var mainMenu = Instantiate(_mainMenuPrefab);
        mainMenu.Initialize(saveLoadController, sceneLoader);

        mainMenu.SettingMenuButton.onClick.AddListener(rootUI.ShowSettingsMenu);
        rootUI.AttachSceneUI(mainMenu, AttachType.AllClear);
    }
}