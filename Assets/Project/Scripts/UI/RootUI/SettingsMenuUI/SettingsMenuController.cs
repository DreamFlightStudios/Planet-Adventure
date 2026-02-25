using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsMenuController : RootUI
{
    [SerializeField] private SettingsConfigurationConfig _defaultConfiguration;

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

    private SaveLoadController _saveLoadController;
    private SettingsMenuModel _model;
    private SettingsMenuView _view;

    [Inject]
    private void Construct(SaveLoadController saveLoadController)
        => _saveLoadController = saveLoadController;

    private void Awake()
    {
        var settings = _saveLoadController.UserData?.SettingsData;
        
        if (settings == null)
        {
            settings = CreateDefaultSettings();
            _saveLoadController.UserData.SettingsData = settings;
        }

        _view = new SettingsMenuView(_container, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _sensivityParameter, _subtitlesParameter, _fullScreenParameter, _graphicsParameter);
        _model = new SettingsMenuModel(_sensivityParameter, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _fullScreenParameter, _subtitlesParameter, _graphicsParameter);

        ApplyParametrs(settings);
        SwitchState(false);
    }

    public override void SwitchState(bool state)
    {
        base.SwitchState(state);

        if (state == false)
        {
            _view.ResetPosition();
            ApplyParametrs(_saveLoadController.UserData.SettingsData);
        }
    }

    public void OnSaveButtonClicked() => ApplyParametrs(_model.GetUserSettingsData());

    public void OnResetButtonClicked() => ApplyParametrs(CreateDefaultSettings());

    private void ApplyParametrs(UserSettingsData settings)
    {
        _saveLoadController.UserData.SettingsData = settings;

        Screen.fullScreen = settings.FullScreen;
        QualitySettings.SetQualityLevel((int)settings.Graphics);

        _saveLoadController.UpdateUserData();
        _view.UpdateView(settings);
    }

    private UserSettingsData CreateDefaultSettings()
    {
        return new UserSettingsData(
        _defaultConfiguration.Sensivity,
        _defaultConfiguration.GraphicsQuality,
        _defaultConfiguration.DialoguesVolume,
        _defaultConfiguration.AmbientVolume,
        _defaultConfiguration.InterfaceVolume,
        _defaultConfiguration.EnvironmentVolume,
        _defaultConfiguration.MusicVolume,
        _defaultConfiguration.IsSubtitlesOn,
        _defaultConfiguration.IsFullScreenOn
        );
    }
}