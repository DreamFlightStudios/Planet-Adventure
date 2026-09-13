using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootControllerUI : MonoBehaviour
{
    [Header("RootUI")]
    [SerializeField] private Transform _canvasRoot;
    [SerializeField] private Transform _sceneContainer;

    [Header("Predefined Screens")]
    [SerializeField] private UIScreen _loadingScreen;
    [SerializeField] private SettingsMenuController _settingsMenu;
    [SerializeField] private GamePopUp _popUp;

    private readonly Dictionary<ScreenId, UIScreen> _registry = new();
    private readonly List<UIScreen> _popupStack = new();

    public GamePopUp PopUp => _popUp;
    public SettingsMenuController SettingsMenu => _settingsMenu;

    public void Initialize(InputSystem input)
    {
        input.UI.Pause.performed += context => OnTriggerInvoke();

        AddScreen(ScreenId.Loading, _loadingScreen, _canvasRoot);
        AddScreen(ScreenId.Settings, _settingsMenu, _canvasRoot);

        if (_popUp != null)
            AddScreen(ScreenId.PopUp, _popUp, _canvasRoot);
        else
            Debug.LogError($"{nameof(GamePopUp)} is not assigned in {nameof(RootControllerUI)}. Popups are disabled.", this);
    }

    public void Register(ScreenId id, UIScreen screen)
        => AddScreen(id, screen, _sceneContainer);

    public void Unregister(ScreenId id)
    {
        if (!_registry.TryGetValue(id, out var screen))
            return;

        _registry.Remove(id);
        _popupStack.Remove(screen);

        screen.DisposeScreen();
        Destroy(screen.gameObject);
    }

    public void Show(ScreenId id)
    {
        var screen = _registry[id];

        if (screen.IsModal && !_popupStack.Contains(screen))
            _popupStack.Add(screen);

        screen.SwitchState(true);
        RefreshOrdering();
    }

    public void Hide(ScreenId id)
    {
        var screen = _registry[id];
        _popupStack.Remove(screen);
        screen.SwitchState(false);
    }

    public bool IsShown(ScreenId id)
        => _registry.TryGetValue(id, out var screen) && screen.IsActive;

    public void ClearSceneScreens()
    {
        foreach (var id in _registry.Where(pair => pair.Value.transform.parent == _sceneContainer).Select(pair => pair.Key).ToArray())
            Unregister(id);

        RemoveHiddenPopups();
    }

    private void AddScreen(ScreenId id, UIScreen screen, Transform container)
    {
        if (_registry.ContainsKey(id))
            Unregister(id);

        screen.Attach(container);
        screen.CreateScreen();

        _registry[id] = screen;
    }

    private ScreenId GetId(UIScreen screen)
        => _registry.First(pair => pair.Value == screen).Key;

    private void CloseTopPopup()
    {
        if (_popupStack.Count == 0)
            return;

        var top = _popupStack[^1];

        if (top.CanCloseByTrigger == false)
            return;

        // The screen may stay open (e.g. waiting for confirmation), so the stack is cleaned by actual state.
        top.RequestClose();
        RemoveHiddenPopups();
    }

    // Screens closed directly through SwitchState bypass Hide and would stay in the stack.
    private void RemoveHiddenPopups()
        => _popupStack.RemoveAll(screen => screen == null || screen.IsActive == false);

    private void OnTriggerInvoke()
    {
        RemoveHiddenPopups();

        if (_popupStack.Count > 0)
        {
            CloseTopPopup();
            return;
        }

        var triggerableScreens = _registry.Values.Where(s => s.IsActivatedByTrigger || s.IsDeactivatedByTrigger);

        var screenToActivate = triggerableScreens.FirstOrDefault(s => s.IsActivatedByTrigger && !s.IsActive);

        if (screenToActivate != null)
        {
            Show(GetId(screenToActivate));
            return;
        }

        var screenToDeactivate = triggerableScreens.FirstOrDefault(s => s.IsDeactivatedByTrigger && s.IsActive);
        screenToDeactivate?.RequestClose();
    }

    private void RefreshOrdering()
    {
        var ordered = _registry.Values.Where(s => s.IsActive).OrderBy(s => s.Layer).ToList();

        for (var i = 0; i < ordered.Count; i++)
            ordered[i].transform.SetSiblingIndex(i);
    }
}
