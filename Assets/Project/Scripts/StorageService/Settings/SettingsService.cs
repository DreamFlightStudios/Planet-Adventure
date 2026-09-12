using System;
using UnityEngine;

public class SettingsService : ISettingsService
{
    private readonly ISaveRepository<UserSettingsData> _repository;
    private readonly SettingsConfigurationConfig _defaultConfiguration;

    private event Action<UserSettingsData> Changed;

    public UserSettingsData Settings => _repository.Data;

    public SettingsService(ISaveRepository<UserSettingsData> repository, SettingsConfigurationConfig defaultConfiguration)
    {
        _repository = repository;
        _defaultConfiguration = defaultConfiguration;
    }

    public static UserSettingsData CreateDefault(SettingsConfigurationConfig configuration)
    {
        return new UserSettingsData
        {
            Sensivity = configuration.Sensivity,
            Graphics = configuration.GraphicsQuality,
            Ambient = configuration.AmbientVolume,
            Dialogues = configuration.DialoguesVolume,
            Interface = configuration.InterfaceVolume,
            Environment = configuration.EnvironmentVolume,
            Music = configuration.MusicVolume,
            Subtitles = configuration.IsSubtitlesOn,
            FullScreen = configuration.IsFullScreenOn,
        };
    }

    public void Initialize()
    {
        _repository.Load();
        ApplyToEngine(Settings);
    }

    public void Subscribe(Action<UserSettingsData> handler)
    {
        Changed += handler;
        handler?.Invoke(Settings);
    }

    public void Unsubscribe(Action<UserSettingsData> handler) => Changed -= handler;

    public void Apply(UserSettingsData settings)
    {
        Settings.CopyFrom(settings);
        _repository.Save();

        ApplyToEngine(Settings);
        Changed?.Invoke(Settings);
    }

    public UserSettingsData CreateDefault() => CreateDefault(_defaultConfiguration);

    private void ApplyToEngine(UserSettingsData settings)
    {
        Screen.fullScreen = settings.FullScreen;
        QualitySettings.SetQualityLevel(Mathf.Clamp((int)settings.Graphics, 0, QualitySettings.names.Length - 1));
    }
}
