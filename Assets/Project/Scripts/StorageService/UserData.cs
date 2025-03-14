using System;

[Serializable]
public class UserData
{
    public string LastSaveName;
    public UserSettingsData Settings;
}
    
[Serializable]
public class UserSettingsData
{
    public string Sensivity;
}