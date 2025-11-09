using TMPro;
using UnityEngine.UI;

public class SettingsMenuView
{
    private readonly Slider AmbientVolumeSlider;
    private readonly Slider DialoguesVolumeSlider;
    private readonly Slider InterfacesVolumeSlider;
    private readonly Slider EnvironmentVolumeSlider;
    private readonly Slider MusicVolumeSlider;
    private readonly Slider SensivitySlider;
    private readonly Toggle SubtitlesToggle;
    private readonly Toggle FullScreenToggle;
    private readonly TMP_Dropdown GraphicsDropdown;

    public SettingsMenuView(Slider ambientVolumeSlider, Slider dialoguesVolumeSlider, Slider interfacesVolumeSlider, Slider environmentVolumeSlider, Slider musicVolumeSlider, Slider sensivitySlider, Toggle subtitlesToggle, Toggle fullScreenToggle, TMP_Dropdown graphicsDropdown)
    {
        AmbientVolumeSlider = ambientVolumeSlider;
        DialoguesVolumeSlider = dialoguesVolumeSlider;
        InterfacesVolumeSlider = interfacesVolumeSlider;
        EnvironmentVolumeSlider = environmentVolumeSlider;
        MusicVolumeSlider = musicVolumeSlider;
        SensivitySlider = sensivitySlider;
        SubtitlesToggle = subtitlesToggle;
        FullScreenToggle = fullScreenToggle;
        GraphicsDropdown = graphicsDropdown;
    }

    public void UpdateView(UserSettingsData userSetingsData)
    {
        AmbientVolumeSlider.value = userSetingsData.Ambient;
        DialoguesVolumeSlider.value = userSetingsData.Dialogues;
        InterfacesVolumeSlider.value = userSetingsData.Interface;
        EnvironmentVolumeSlider.value = userSetingsData.Environment;
        MusicVolumeSlider.value = userSetingsData.Music;
        SensivitySlider.value = userSetingsData.Sensivity;
        SubtitlesToggle.isOn = userSetingsData.Subtitles;
        FullScreenToggle.isOn = userSetingsData.FullScreen;
        GraphicsDropdown.value = (int)userSetingsData.Graphics;
    }
}