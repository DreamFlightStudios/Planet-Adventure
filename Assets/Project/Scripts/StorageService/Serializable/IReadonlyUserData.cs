using System.Collections.Generic;

public interface IReadonlyUserData
{
    Dictionary<string, bool> LevelsData { get; }
    UserSettingsData SettingsData { get; }
}