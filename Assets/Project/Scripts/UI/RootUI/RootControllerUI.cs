using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootControllerUI : MonoBehaviour
{
    [Header("RootUI")]
    [SerializeField] private Transform _sceneUIContainer;

    [Header("RootUI")]
    [SerializeField] private RootUI _loadingScreen;
    [SerializeField] public RootUI _settingsMenu;

    private List<AttachableContainerUI> _addedUIÑontainers = new List<AttachableContainerUI>();
    private Dictionary<DynamicUI, int> _dynamicUI = new Dictionary<DynamicUI, int>();

    public void Initialize(InputSystem input)
    {
        input.UI.Pause.performed += context => OnTrigerInvoke();
        _dynamicUI.Add(_settingsMenu, _settingsMenu.LayerPriority);
    }

    public void ShowLoadingScreen() 
        => _loadingScreen.SwitchState(true);

    public void HideLoadingScreen() 
        => _loadingScreen.SwitchState(false);

    public void ShowSettingsMenu() 
        => _settingsMenu.SwitchState(true);

    public void AttachSceneUI(AttachableContainerUI sceneUI, AttachType type = AttachType.Default)
    {
        if (type == AttachType.AllClear)
            ClearSceneUI();

        if (sceneUI.IsDeactivatedByTrigger || sceneUI.IsActivatedByTrigger)
        {
            _dynamicUI.Add(sceneUI, sceneUI.LayerPriority);
            _dynamicUI.OrderByDescending(key => key.Value);
        }

        sceneUI.Attach(_sceneUIContainer);
        _addedUIÑontainers.Add(sceneUI);
    }

    public void ClearSceneUI()
    {
        foreach (var container in _addedUIÑontainers)
        {
            _dynamicUI.Remove(container);
            Destroy(container.gameObject);
        }

        _addedUIÑontainers.Clear();
    }

    private void OnTrigerInvoke()
    {
        var uiToActivate = _dynamicUI.Keys.FirstOrDefault(ui => ui.IsActivatedByTrigger && !ui.IsActive);

        if (uiToActivate)
        {
            uiToActivate.SwitchState(true);
            return;
        }

        var uiToDeactivate = _dynamicUI.Keys.FirstOrDefault(ui => ui.IsActive);

        if (uiToDeactivate)
        {
            uiToDeactivate.SwitchState(false);
            return;
        }
    }
}

public enum AttachType
{
    Default,
    AllClear
}