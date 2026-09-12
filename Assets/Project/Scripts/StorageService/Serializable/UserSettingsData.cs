using System;
using UnityEngine.Scripting;

[Serializable]
[Preserve]
public class UserSettingsData : SaveData
{
    public const int CurrentVersion = 1;

    public float Sensivity;
    public GraphicsQualityType Graphics;

    public float Ambient;
    public float Dialogues;
    public float Interface;
    public float Environment;
    public float Music;

    public bool Subtitles;
    public bool FullScreen;

    public void CopyFrom(UserSettingsData other)
    {
        if (other == null || other == this)
            return;

        Sensivity = other.Sensivity;
        Graphics = other.Graphics;

        Ambient = other.Ambient;
        Dialogues = other.Dialogues;
        Interface = other.Interface;
        Environment = other.Environment;
        Music = other.Music;

        Subtitles = other.Subtitles;
        FullScreen = other.FullScreen;
    }
}
