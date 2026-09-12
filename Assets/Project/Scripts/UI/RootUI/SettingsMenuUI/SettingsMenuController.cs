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

    private ISettingsService _settingsService;
    private SettingsMenuModel _model;
    private SettingsMenuView _view;

    [Inject]
    private void Construct(ISettingsService settingsService)
        => _settingsService = settingsService;

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

    public void OnSaveButtonClicked() => _settingsService.Apply(_model.GetUserSettingsData());

    public void OnResetButtonClicked()
    {
        var defaultSettings = _settingsService.CreateDefault();

        _settingsService.Apply(defaultSettings);
        _view.UpdateView(defaultSettings);
    }
}
