using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsConfigurationConfig _defaultConfiguration;

    [Header("Control Setup")]
    [SerializeField] private Slider _sensivity;

    [Header("Graphics Setup")]
    [SerializeField] private TMP_Dropdown _resolution;
    [SerializeField] private Toggle _fullScreen;
    [SerializeField] private TMP_Dropdown _graphics;

    [Header("Audio Setup")]
    [SerializeField] private Slider _ambient;
    [SerializeField] private Slider _dialogues;
    [SerializeField] private Slider _interface;
    [SerializeField] private Slider _environment;
    [SerializeField] private Slider _music;

    [Header("Language Setup")]
    [SerializeField] private Toggle _subtitles;

    [Header("Audio Mixer Setup")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _ambientMixerKey;
    [SerializeField] private string _dialoguesMixerKey;
    [SerializeField] private string _interfaceMixerKey;
    [SerializeField] private string _environmentMixerKey;
    [SerializeField] private string _musicMixerKey;

    private SaveLoadController _saveLoadController;
    private SettingsMenuView _view;

    [Inject]
    private void Construct(SaveLoadController saveLoadController) 
        => _saveLoadController = saveLoadController;

    private void Awake()
    {
        _view = new(transform.gameObject, _ambient, _dialogues, _interface, _environment, _music, _sensivity, _subtitles, _fullScreen, _graphics);

        if (_saveLoadController.UserData.SettingsData == null)
        {
            _saveLoadController.UserData.SettingsData = new();
            ResetParametrs();
        }
        else
            ApplyParametrs();
    }

    public void OnSaveButtonClicked() 
        => SaveParametrs();

    public void OnResetButtonClicked() 
        => ResetParametrs();

    public void OnBackButtonClicked() 
        => gameObject.SetActive(false);

    private void SaveParametrs()
    {
        var userSetingsData = _saveLoadController.UserData.SettingsData;

        userSetingsData.Ambient = _ambient.value;
        userSetingsData.Dialogues = _dialogues.value;
        userSetingsData.Interface = _interface.value;
        userSetingsData.Environment = _environment.value;
        userSetingsData.Music = _music.value;
        userSetingsData.Sensivity = _sensivity.value;
        userSetingsData.Subtitles = _subtitles;
        userSetingsData.FullScreen = _fullScreen;
        userSetingsData.Graphics = (GraphicsQualityType)_graphics.value;

        _saveLoadController.SaveUserData();
        _view.UpdateView(_saveLoadController.UserData.SettingsData);
        ApplyParametrs();
    }

    private void ApplyParametrs()
    {
        var userSetingsData = _saveLoadController.UserData.SettingsData;

        _mixer.SetFloat(_ambientMixerKey, userSetingsData.Ambient);
        _mixer.SetFloat(_dialoguesMixerKey, userSetingsData.Dialogues);
        _mixer.SetFloat(_interfaceMixerKey, userSetingsData.Interface);
        _mixer.SetFloat(_environmentMixerKey, userSetingsData.Environment);
        _mixer.SetFloat(_musicMixerKey, userSetingsData.Music);

        Screen.fullScreen = userSetingsData.FullScreen;
        QualitySettings.SetQualityLevel((int)userSetingsData.Graphics);

        Debug.Log("Parametrs");
    }

    private void ResetParametrs()
    {
        _ambient.value = _defaultConfiguration.AmbientVolume;
        _dialogues.value = _defaultConfiguration.DialoguesVolume;
        _interface.value = _defaultConfiguration.InterfaceVolume;
        _environment.value = _defaultConfiguration.EnvironmentVolume;
        _music.value = _defaultConfiguration.MusicVolume;
        _sensivity.value = _defaultConfiguration.Sensivity;
        _fullScreen.isOn = _defaultConfiguration.IsFullScreenOn;
        _subtitles.isOn = _defaultConfiguration.IsSubtitlesOn;
        SaveParametrs();

        Debug.Log("ResetParametrs");
    }

    private void OnEnable() 
        => _view.UpdateView(_saveLoadController.UserData.SettingsData);
}