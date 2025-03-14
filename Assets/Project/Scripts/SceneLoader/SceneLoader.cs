using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    public event Action LoadStarted;
    public event Action LoadFinished;

    private SaveLoadController _saveLoadController;
    private StorableContainer _stororableContainer;

    [Inject]
    private void Construct(RootViewUI rootUI, SaveLoadController saveLoadController)
    {
        LoadStarted += rootUI.ShowLoadingScreen;
        LoadFinished += rootUI.HideLoadingScreen;

        _saveLoadController = saveLoadController;
    }

    public void LoadGameplayScene(string saveName = null)
    {
        LoadStarted?.Invoke();

        StartCoroutine(LoadScene(SceneID.Scene1));

        var data = _saveLoadController.GetGameSave(saveName);

        if (data != null)
        {
            foreach (var interactiveObject in _stororableContainer.InteractiveObjects)
            {
                Debug.Log("JK" + interactiveObject.Id);
                if (data.InteractiveObjectsData.ContainsKey(interactiveObject.Id))
                {
                    interactiveObject.SetData(data.InteractiveObjectsData[interactiveObject.Id]);
                    Debug.Log("Loaded");
                }
            }
        }

        LoadFinished?.Invoke();
    }

    public void LoadMainMenu()
    {
        LoadStarted?.Invoke();

        StartCoroutine(LoadScene(SceneID.MainMenu));

        LoadFinished?.Invoke();
    }

    public void SaveGameplayScene()
    {
        GameData data = new GameData();

        foreach (var interactiveObject in _stororableContainer.InteractiveObjects)
        {
            data.InteractiveObjectsData.Add(interactiveObject.Id, interactiveObject.GetData());
            Debug.Log("Saved");
        }

        _saveLoadController.SaveGameData(data);
    }

    private IEnumerator LoadScene(SceneID id)
    {
        yield return SceneManager.LoadSceneAsync((int)id);
        yield return new WaitForSecondsRealtime(1f);
    }
}