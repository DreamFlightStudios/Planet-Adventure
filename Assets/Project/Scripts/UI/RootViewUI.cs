using UnityEngine;

public class RootViewUI : MonoBehaviour
{
    [SerializeField] private Transform _uiSceneContainer;
    [SerializeField] private GameObject _loadingScreen;

    private void Awake() => ShowLoadingScreen();

    public void ShowLoadingScreen() => _loadingScreen.SetActive(true);

    public void HideLoadingScreen() => _loadingScreen.SetActive(false);

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