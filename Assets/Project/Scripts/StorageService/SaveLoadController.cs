using System;

public class SaveLoadController
{
    private const string UserDataPath = "UserData";
    private readonly IStorageService Service;

    public event Action<UserData> Loaded;
    public UserData UserData { get; private set; }

    public SaveLoadController()
    {
        Service = new BinarySaver();

        if (Service.FileExists(UserDataPath) == false)
            Service.Save(UserDataPath, new UserData());

        UserData = Service.Load<UserData>(UserDataPath);
    }

    public void UpdateUserData()
    {
        Service.Save(UserDataPath, UserData);
        Loaded?.Invoke(UserData);
    }

    public void InitializeGameplayLevel(LevelData data)
    {
        var levelsData = UserData.LevelsData;

        if (levelsData.ContainsKey(data.LevelId))
            return;

        levelsData.Add(data.LevelId, data.IsUnlocked);
    }

    public void UnlockGameplayLevel(string LevelId)
    {
        var levelsData = UserData.LevelsData;

        if (levelsData.ContainsKey(LevelId))
        {
            levelsData[LevelId] = true;
            UpdateUserData();
        }
    }
}