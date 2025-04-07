using System;

public class SaveLoadController
{
    private const string UserDataPath = "UserData";

    private readonly IStorageService Service;
    public UserData UserData { get; private set; }

    public SaveLoadController()
    {
        Service = new BinarySaver();

        if (Service.FileExists(UserDataPath) == false)
            Service.Save(UserDataPath, new UserData());

        UserData = Service.Load<UserData>(UserDataPath);
    }

    public void SaveUserData() => Service.Save(UserDataPath, UserData);

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
            SaveUserData();
        }
    }
}