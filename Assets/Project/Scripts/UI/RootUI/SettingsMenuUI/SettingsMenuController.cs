using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsMenuController : UIScreen
{
    [Header("Components")]
    [SerializeField] private Transform _container;

    [Header("Control Setup")]
    [SerializeField] private Slider _sensivityParameter;

    [Header("Graphics Setup")]
    [SerializeField] private TMP_Dropdown _resolutionParameter;
    [SerializeField] private TMP_Dropdown _graphicsParameter;
    [SerializeField] private Toggle _fullScreenParameter;

    [Header("Audio Setup")]
    [SerializeField] private Slider _ambientVolumeParameter;
    [SerializeField] private Slider _dialoguesVolumeParameter;
    [SerializeField] private Slider _interfaceVolumeParameter;
    [SerializeField] private Slider _environmentVolumeParameter;
    [SerializeField] private Slider _musicVolumeParameter;

    [Header("Language Setup")]
    [SerializeField] private Toggle _subtitlesParameter;

    [Header("PopUps")]
    [SerializeField] private PopUpConfig _unsavedChangesPopUp;
    [SerializeField] private PopUpConfig _resetSettingsPopUp;

    private ISettingsService _settingsService;
    private IPopUpService _popUpService;
    private SettingsMenuModel _model;
    private SettingsMenuView _view;

    [Inject]
    private void Construct(ISettingsService settingsService)
        => _settingsService = settingsService;

    // RootUI is created before IPopUpService is bound, so the service is passed from ProjectInstaller.
    public void Initialize(IPopUpService popUpService)
        => _popUpService = popUpService;

    private void Awake()
    {
        _view = new SettingsMenuView(_container, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _sensivityParameter, _subtitlesParameter, _fullScreenParameter, _graphicsParameter);
        _model = new SettingsMenuModel(_sensivityParameter, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _fullScreenParameter, _subtitlesParameter, _graphicsParameter);

        _view.UpdateView(_settingsService.Settings);
        SwitchState(false);
    }

    public override void SwitchState(bool state)
    {
        base.SwitchState(state);

        if (state == false)
        {
            _view.ResetPosition();
            _view.UpdateView(_settingsService.Settings);
        }
    }

    public override void RequestClose()
    {
        if (HasUnsavedChanges() == false)
        {
            SwitchState(false);
            return;
        }

        _popUpService.Show(_unsavedChangesPopUp, OnUnsavedChangesPopUpClosed);
    }

    public void OnSaveButtonClicked() => _settingsService.Apply(_model.GetUserSettingsData());

    public void OnResetButtonClicked() => _popUpService.Show(_resetSettingsPopUp, OnResetSettingsPopUpClosed);

    private bool HasUnsavedChanges()
        => _model.GetUserSettingsData().IsSameAs(_settingsService.Settings) == false;

    private void OnUnsavedChangesPopUpClosed(PopUpResult result)
    {
        if (result == PopUpResult.Closed)
            return;

        if (result == PopUpResult.Accepted)
            OnSaveButtonClicked();

        SwitchState(false);
    }

    private void OnResetSettingsPopUpClosed(PopUpResult result)
    {
        if (result != PopUpResult.Accepted)
            return;

        var defaultSettings = _settingsService.CreateDefault();

        _settingsService.Apply(defaultSettings);
        _view.UpdateView(defaultSettings);
    }
}
