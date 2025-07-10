using UnityEngine;

public class RootViewUI : MonoBehaviour
{
    [SerializeField] private Transform _uiSceneContainer;
    [SerializeField] private LoadingScreenUI _loadingScreen;
    [SerializeField] private SettingsMenuController _settingsMenu;

    private void Awake()
    {
        HideSettingsMenu();
        ShowLoadingScreen();
    }

    public void ShowLoadingScreen() => _loadingScreen.OnLoadStarted();

    public void HideLoadingScreen() => _loadingScreen.OnLoadFinished();

    public void ShowSettingsMenu()
    {
        //_settingsMenu.OnShowButtonClicked();
    }

    public void HideSettingsMenu()
    {
        //_settingsMenu.OnShowButtonClicked();
    }

    public void AttachSceneUI(GameObject sceneUI, AttachType type = AttachType.Default)
    {
        if (type == AttachType.AllClear)
            ClearSceneUI();

        sceneUI.transform.SetParent(_uiSceneContainer, false);
    }

    public void ClearSceneUI()
    {
        var childCount = _uiSceneContainer.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(_uiSceneContainer.GetChild(i).gameObject);
        }
    }
}

public enum AttachType
{
    Default,
    AllClear
}