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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            StartCoroutine(SaveGameplayData());
    }

    [Inject]
    private void Construct(RootViewUI rootUI, SaveLoadController saveLoadController)
    {
        LoadStarted += rootUI.ShowLoadingScreen;
        LoadFinished += rootUI.HideLoadingScreen;

        _saveLoadController = saveLoadController;
    }

    public void Initialize(StorableContainer storableContainer)
    {
        _stororableContainer = storableContainer;
        StartCoroutine(LoadGameplayData(_saveLoadController.GetLastGameSave()));
    }

    public void LoadGameplayScene(LoadType loadType = LoadType.Default, string saveName = null)
    {
        LoadStarted?.Invoke();

        if (loadType == LoadType.CreateGame)
            _saveLoadController.CreateGameSave();
        else
            _saveLoadController.GetGameSave(saveName);

        StartCoroutine(LoadScene(SceneID.Scene1));
        LoadFinished?.Invoke();
    }

    public IEnumerator LoadGameplayData(GameData data)
    {
        LoadStarted?.Invoke();

        if (data != null)
        {
            foreach (var interactiveObject in _stororableContainer.InteractiveObjects)
            {
                if (data.InteractiveObjectsData.ContainsKey(interactiveObject.Id))
                {
                    interactiveObject.SetData(data.InteractiveObjectsData[interactiveObject.Id]);
                    yield return null;
                }
            }
        }

        LoadFinished?.Invoke();
    }

    public IEnumerator SaveGameplayData()
    {
        LoadStarted?.Invoke();

        var data = new GameData();
        foreach (var interactiveObject in _stororableContainer.InteractiveObjects)
        {
            data.InteractiveObjectsData.Add(interactiveObject.Id, interactiveObject.GetData());
            yield return null;
        }
        _saveLoadController.SaveGameData(data);

        LoadFinished?.Invoke();
    }

    public void LoadMainMenu()
    {
        LoadStarted?.Invoke();

        StartCoroutine(LoadScene(SceneID.MainMenu));

        LoadFinished?.Invoke();
    }

    private IEnumerator LoadScene(SceneID id)
    {
        yield return SceneManager.LoadSceneAsync((int)id);
        yield return new WaitForSecondsRealtime(1f);
    }
}

public enum LoadType
{
    CreateGame,
    Default
}