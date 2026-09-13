using System;
using UnityEngine;
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

    public bool IsSameAs(UserSettingsData other)
    {
        if (other == null)
            return false;

        return Mathf.Approximately(Sensivity, other.Sensivity)
            && Graphics == other.Graphics
            && Mathf.Approximately(Ambient, other.Ambient)
            && Mathf.Approximately(Dialogues, other.Dialogues)
            && Mathf.Approximately(Interface, other.Interface)
            && Mathf.Approximately(Environment, other.Environment)
            && Mathf.Approximately(Music, other.Music)
            && Subtitles == other.Subtitles
            && FullScreen == other.FullScreen;
    }
}
