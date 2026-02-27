using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    public event Action LoadStarted;
    public event Action LoadFinished;

    [Inject]
    private void Construct(RootControllerUI rootUI)
    {
        LoadStarted += rootUI.ShowLoadingScreen;
        LoadFinished += rootUI.HideLoadingScreen;
    }

    public void ChangeScene(string sceneName) 
        => StartCoroutine(LoadScene(sceneName));

    private IEnumerator LoadScene(string sceneName)
    {
        LoadStarted?.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        yield return SceneManager.LoadSceneAsync(sceneName);
        LoadFinished?.Invoke();
    }
}