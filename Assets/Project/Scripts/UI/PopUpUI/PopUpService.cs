using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpService : IPopUpService
{
    private readonly GamePopUp _view;
    private readonly RootControllerUI _rootUI;
    private readonly IPauseService _pauseService;

    private readonly Queue<(PopUpConfig Config, Action<PopUpResult> Callback)> _requests = new();

    private Action<PopUpResult> _currentCallback;
    private PopUpResult _result;

    public bool IsShown { get; private set; }

    public PopUpService(GamePopUp view, RootControllerUI rootUI, IPauseService pauseService, SceneLoader sceneLoader)
    {
        _view = view;
        _rootUI = rootUI;
        _pauseService = pauseService;

        sceneLoader.LoadStarted += OnLoadStarted;

        if (_view == null)
            return;

        _view.ResultSelected += OnResultSelected;
        _view.Hidden += OnHidden;
    }

    public void Show(PopUpConfig config, Action<PopUpResult> onClosed = null)
    {
        if (_view == null || config == null)
        {
            Debug.LogError($"Cannot show popup: {(_view == null ? "view is not assigned" : "config is null")}.");
            return;
        }

        _requests.Enqueue((config, onClosed));
        ShowNext();
    }

    private void ShowNext()
    {
        if (IsShown || _requests.Count == 0)
            return;

        var request = _requests.Dequeue();

        IsShown = true;
        _currentCallback = request.Callback;
        _result = PopUpResult.Closed;

        _view.Setup(request.Config);
        _pauseService.Pause(this);
        _rootUI.Show(ScreenId.PopUp);
    }

    private void OnResultSelected(PopUpResult result)
    {
        _result = result;
        _rootUI.Hide(ScreenId.PopUp);
    }

    private void OnHidden()
    {
        if (IsShown == false)
            return;

        IsShown = false;
        _pauseService.Resume(this);

        var callback = _currentCallback;
        _currentCallback = null;
        callback?.Invoke(_result);

        ShowNext();
    }

    // Callbacks belong to objects of the unloading scene, so they are dropped without being invoked.
    private void OnLoadStarted()
    {
        _requests.Clear();
        _currentCallback = null;

        if (IsShown)
            _rootUI.Hide(ScreenId.PopUp);
    }
}
