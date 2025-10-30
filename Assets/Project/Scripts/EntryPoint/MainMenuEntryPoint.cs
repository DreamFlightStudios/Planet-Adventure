using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuPrefab;

    [Inject]
    private void Construct(RootContainerUI rootUI, SaveLoadController saveLoadController, SceneLoader sceneLoader)
    {
        var mainMenu = Instantiate(_mainMenuPrefab);
        mainMenu.Initialize(saveLoadController, sceneLoader, rootUI.SettingsMenu);

        rootUI.AttachSceneUI(mainMenu.gameObject, AttachType.AllClear);
    }
}