using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public Dictionary<string, bool> LevelsData = new();
    public UserSettingsData SettingsData;
}
    
[Serializable]
public class UserSettingsData
{
    public int Sensivity;
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