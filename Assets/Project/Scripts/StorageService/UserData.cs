using System.Collections.Generic;
using System;

[Serializable]
public class UserData
{
    public Dictionary<string, bool> LevelsData = new();
    public UserSettingsData SettingsData;
}

[Serializable]
public class UserSettingsData
{
    public float Sensivity;
    public GraphicsQualityType Graphics;
    public int Resolution;
    public float Dialogues;
    public float Ambient;
    public float Interface;
    public float Environment;
    public float Music;
    public bool Subtitles;
    public bool FullScreen;
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