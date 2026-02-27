using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuModel
{
    private readonly Slider SensivityParameter;
    private readonly Slider AmbientVolumeParameter;
    private readonly Slider DialoguesVolumeParameter;
    private readonly Slider InterfaceVolumeParameter;
    private readonly Slider EnvironmentVolumeParameter;
    private readonly Slider MusicVolumeParameter;
    private readonly Toggle FullScreenParameter;
    private readonly Toggle SubtitlesParameter;
    private readonly TMP_Dropdown GraphicsParameter;

    public float Sensivity { get => SensivityParameter.value; set => SensivityParameter.value = value; }
    public float AmbientVolume { get => AmbientVolumeParameter.value; set => AmbientVolumeParameter.value = value; }
    public float DialoguesVolume { get => DialoguesVolumeParameter.value; set => DialoguesVolumeParameter.value = value; }
    public float InterfaceVolume { get => InterfaceVolumeParameter.value; set => InterfaceVolumeParameter.value = value; }
    public float EnvironmentVolume { get => EnvironmentVolumeParameter.value; set => EnvironmentVolumeParameter.value = value; }
    public float MusicVolume { get => MusicVolumeParameter.value; set => MusicVolumeParameter.value = value; }
    public bool FullScreen { get => FullScreenParameter.isOn; set => FullScreenParameter.isOn = value; }
    public bool Subtitles { get => SubtitlesParameter.isOn; set => SubtitlesParameter.isOn = value; }
    public GraphicsQualityType Graphics { get => (GraphicsQualityType)GraphicsParameter.value; set => GraphicsParameter.value = (int)value; }

    public SettingsMenuModel(Slider sensivityParameter, Slider ambientVolumeParameter, Slider dialoguesVolumeParameter, Slider interfaceVolumeParameter, Slider environmentVolumeParameter, Slider musicVolumeParameter, Toggle fullScreenParameter, Toggle subtitlesParameter, TMP_Dropdown graphicsParameter)
    {
        SensivityParameter = sensivityParameter;
        AmbientVolumeParameter = ambientVolumeParameter;
        DialoguesVolumeParameter = dialoguesVolumeParameter;
        InterfaceVolumeParameter = interfaceVolumeParameter;
        EnvironmentVolumeParameter = environmentVolumeParameter;
        MusicVolumeParameter = musicVolumeParameter;
        FullScreenParameter = fullScreenParameter;
        SubtitlesParameter = subtitlesParameter;
        GraphicsParameter = graphicsParameter;
    }

    public UserSettingsData GetUserSettingsData()
    {
        return new UserSettingsData(
        Sensivity,
        Graphics,
        DialoguesVolume,
        AmbientVolume,
        InterfaceVolume,
        EnvironmentVolume,
        MusicVolume,
        Subtitles,
        FullScreen);
    }
}
