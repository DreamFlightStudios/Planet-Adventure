using UnityEngine;

public class RootViewUI : MonoBehaviour
{
    [SerializeField] private Transform _uiSceneContainer;
    [SerializeField] private LoadingScreenUI _loadingScreen;

    private void Awake() => ShowLoadingScreen();

    public void ShowLoadingScreen() => _loadingScreen.OnLoadStarted();

    public void HideLoadingScreen() => _loadingScreen.OnLoadFinished();

    public void AttachSceneUI(GameObject sceneUI)
    {
        ClearSceneUI();
        sceneUI.transform.SetParent(_uiSceneContainer, false);
    }

    private void ClearSceneUI()
    {
        var childCount = _uiSceneContainer.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(_uiSceneContainer.GetChild(i).gameObject);
        }
    }
}