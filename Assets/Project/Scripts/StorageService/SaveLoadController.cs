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

    public void SaveUserData()
        => Service.Save(UserDataPath, UserData);

    public GameData GetGameSave(string saveName = null)
    {
        if (saveName != null)
        {
            UserData.LastSaveName = saveName;
            SaveUserData();

            return 
                Service.Load<GameData>(saveName);
        }
        else
        {
            if (UserData.LastSaveName != null)
                return Service.Load<GameData>(UserData.LastSaveName);

            return null;
        }
    }

    public void SaveGameData(GameData data, string saveName = null)
    {
        if (saveName != null)
        {
            UserData.LastSaveName = saveName;

            Service.Save(saveName, data);
            SaveUserData();
        }
        else
        {
            if (UserData.LastSaveName == null)
                UserData.LastSaveName = "New Save";

            Service.Save(UserData.LastSaveName, data);
        }
    }
}