using UnityEngine;

public class JsonSaveSerializer : ISaveSerializer
{
    public string Serialize(object data) => JsonUtility.ToJson(data, true);

    public void Populate<T>(string content, T target) where T : class
        => JsonUtility.FromJsonOverwrite(content, target);
}
