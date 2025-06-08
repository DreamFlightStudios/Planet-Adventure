using UnityEngine;

[CreateAssetMenu(fileName = "New Settings Configuration Config", menuName = "Configs/System/Create Settings Configuration Config")]
public class SettingsConfigurationConfig : ScriptableObject
{
    [Header("Control")]
    [field: SerializeField] public float Sensivity {  get; private set; }

    [Header("Volume")]
    [field: SerializeField] public float AmbientVolume { get; private set; }
    [field: SerializeField] public float DialoguesVolume { get; private set; }
    [field: SerializeField] public float InterfaceVolume { get; private set; }
    [field: SerializeField] public float EnvironmentVolume { get; private set; }
    [field: SerializeField] public float MusicVolume { get; private set; }

    [Header("Graphics")]
    [field: SerializeField] public GraphicsQualityType GraphicsQuality { get; private set; }

    [Header("Video")]
    [field: SerializeField] public bool IsSubtitlesOn { get; private set; }
    [field: SerializeField] public bool IsFullScreenOn { get; private set; }
}