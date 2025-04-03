using System;

public class SaveLoadController
{
    private const string UserDataPath = "UserData";

    public UserData UserData { get; private set; }
    private readonly IStorageService Service;

    public SaveLoadController()
    {
        Service = new BinarySaver();

        if (Service.FileExists(UserDataPath) == false)
            Service.Save(UserDataPath, new UserData());

        UserData = Service.Load<UserData>(UserDataPath);
    }

    public void SaveUserData() => Service.Save(UserDataPath, UserData);

    public GameData CreateGameSave()
    {
        var saveName = DateTime.Now.ToString("dd/MM/yyyy hh/mm/ss");

        var data = new GameData();
        Service.Save(saveName, data);

        UserData.LastSaveName = saveName;
        SaveUserData();

        return data;
    }

    public GameData GetGameSave(string saveName)
    {
        UserData.LastSaveName = saveName;
        SaveUserData();

        return 
            Service.Load<GameData>(saveName);
    }

    public GameData GetLastGameSave() => Service.Load<GameData>(UserData.LastSaveName);

    public void SaveGameData(GameData data) => Service.Save(UserData.LastSaveName, data);
}