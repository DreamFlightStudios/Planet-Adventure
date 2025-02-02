using UnityEngine;
using Zenject;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuPrefab;

    [Inject]
    private void Construct(RootViewUI rootUI, SceneLoader sceneLoader)
    {
        var mainMenu = Instantiate(_mainMenuPrefab);
        mainMenu.Initialize(sceneLoader);

        rootUI.AttachSceneUI(mainMenu.gameObject);
    }
}