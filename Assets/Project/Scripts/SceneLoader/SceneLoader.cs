using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    private event Action LoadStarted;
    private event Action LoadFinished;

    [Inject]
    private void Construct(RootViewUI rootUI)
    {
        LoadStarted += rootUI.ShowLoadingScreen;
        LoadFinished += rootUI.HideLoadingScreen;
    }

    public void LoadGameplayScene() => StartCoroutine(LoadScene(SceneID.Scene1));

    public void LoadMainMenu() => StartCoroutine(LoadScene(SceneID.MainMenu));

    private IEnumerator LoadScene(SceneID id)
    {
        LoadStarted?.Invoke();

        yield return SceneManager.LoadSceneAsync((int)id);
        yield return new WaitForSecondsRealtime(1f);

        LoadFinished?.Invoke();
    }
}