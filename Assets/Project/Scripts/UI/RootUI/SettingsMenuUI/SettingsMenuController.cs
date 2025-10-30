using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsConfigurationConfig _defaultConfiguration;

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

    [Header("Audio Mixer Setup")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _ambientMixerKey;
    [SerializeField] private string _dialoguesMixerKey;
    [SerializeField] private string _interfaceMixerKey;
    [SerializeField] private string _environmentMixerKey;
    [SerializeField] private string _musicMixerKey;

    private SaveLoadController _saveLoadController;
    private SettingsMenuView _view;
    private SettingsMenuModel _model;

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

        _view = new SettingsMenuView(transform.gameObject, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _sensivityParameter, _subtitlesParameter, _fullScreenParameter, _graphicsParameter);
        _model = new SettingsMenuModel(_sensivityParameter, _ambientVolumeParameter, _dialoguesVolumeParameter, _interfaceVolumeParameter, _environmentVolumeParameter, _musicVolumeParameter, _fullScreenParameter, _subtitlesParameter, _graphicsParameter);

        ApplyParametrs();
        _view.ChangeState(false);
    }

    public void OnShowButtonClicked()
        => _view.ChangeState(true);

    public void OnHideButtonClicked()
    => _view.ChangeState(false);

    public void OnSaveButtonClicked()
    {
        _saveLoadController.UserData.SettingsData = new UserSettingsData(
        _model.Sensivity,
        _model.Graphics,
        _model.DialoguesVolume,
        _model.AmbientVolume,
        _model.InterfaceVolume,
        _model.EnvironmentVolume,
        _model.MusicVolume,
        _model.Subtitles,
        _model.FullScreen);

        _saveLoadController.SaveUserData();
        ApplyParametrs();
    }

    public void OnResetButtonClicked()
    {
        _saveLoadController.UserData.SettingsData = CreateDefaultSettings();
        ApplyParametrs();
    }

    public void OnBackButtonClicked()
    {
        gameObject.SetActive(false);
        ApplyParametrs();
    }

    private void ApplyParametrs()
    {
        var settings = _saveLoadController.UserData.SettingsData;

        _mixer.SetFloat(_ambientMixerKey, settings.Ambient);
        _mixer.SetFloat(_dialoguesMixerKey, settings.Dialogues);
        _mixer.SetFloat(_interfaceMixerKey, settings.Interface);
        _mixer.SetFloat(_environmentMixerKey, settings.Environment);
        _mixer.SetFloat(_musicMixerKey, settings.Music);

        Screen.fullScreen = settings.FullScreen;
        QualitySettings.SetQualityLevel((int)settings.Graphics);

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