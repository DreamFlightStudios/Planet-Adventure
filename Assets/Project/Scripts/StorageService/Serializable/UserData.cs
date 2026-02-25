using System.Collections.Generic;
using System;

[Serializable]
public class UserData : IReadonlyUserData
{
    public Dictionary<string, bool> LevelsData
    {
        get
        {
            return _levelsData;
        }
        set
        {
            if (value != null && value != _levelsData)
                _levelsData = value;
        }
    }
    public UserSettingsData SettingsData
    {
        get
        {
            return _settingsData;
        }
        set
        {
            if (value != null && value != _settingsData)
                _settingsData = value;
        }
    }

    private Dictionary<string, bool> _levelsData = new();
    private UserSettingsData _settingsData;
}

[Serializable]
public class UserSettingsData
{
    public readonly float Sensivity;
    public readonly GraphicsQualityType Graphics;

    public readonly float Ambient;
    public readonly float Dialogues;
    public readonly float Interface;
    public readonly float Environment;
    public readonly float Music;

    public readonly bool Subtitles;
    public readonly bool FullScreen;

    public UserSettingsData(float sensivity, GraphicsQualityType graphics, float dialogues, float ambient, float interfaceVolume, float environmentVolume, float musicVolume, bool subtitles, bool fullScreen)
    {
        Sensivity = sensivity;
        Graphics = graphics;
        Dialogues = dialogues;
        Ambient = ambient;
        Interface = interfaceVolume;
        Environment = environmentVolume;
        Music = musicVolume;
        Subtitles = subtitles;
        FullScreen = fullScreen;
    }
}

[Serializable]
public class LevelData
{
    public LevelData(string levelId, bool isUnlocked = false)
    {
        LevelId = levelId;
        IsUnlocked = isUnlocked;
    }

    public bool IsUnlocked;
    public string LevelId;
}