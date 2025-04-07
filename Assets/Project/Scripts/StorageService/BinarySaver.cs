using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaver : IStorageService
{
    private readonly string _location;
    private readonly BinaryFormatter _formatter;
    private const string FileExtension = ".mfs";

    public BinarySaver()
    {
        _location = Application.persistentDataPath + "/Saves";
        Directory.CreateDirectory(_location);

        _formatter = GetFormatter();
    }

    public IEnumerable<string> GetAllSaves() => Directory.GetFiles(_location, "*" + FileExtension);

    public void Save(string key, object data)
    {
        string path = BuildPath(key);
        try
        {
            using (FileStream file = File.Create(path))
            {
                _formatter.Serialize(file, data);
            }
            Debug.Log($"Successfully saved to: {path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save to: {path}.  Error: {e}");
        }
    }

    public T Load<T>(string key) where T : class
    {
        string path = BuildPath(key);

        if (!FileExists(key))
            return null;

        try
        {
            using (FileStream file = File.Open(path, FileMode.Open))
            {
                Debug.Log($"Successfully loaded from: {path}");
                return _formatter.Deserialize(file) as T;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load from: {path}.  Error: {e}");
            return null;
        }
    }

    public bool Delete(string key)
    {
        string path = BuildPath(key);

        if (!File.Exists(path)) 
            return false;

        try
        {
            File.Delete(path);
            Debug.Log($"Successfully deleted: {path}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to delete: {path}.  Error: {e}");
            return false;
        }
    }

    public bool FileExists(string key) => File.Exists(BuildPath(key));

    private BinaryFormatter GetFormatter()
    {
        var formatter = new BinaryFormatter();
        var surrogateSelector = new SurrogateSelector();

        surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), new Vector3Serializer());
        surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), new QuaternionSerializer());
        formatter.SurrogateSelector = surrogateSelector;

        return formatter;
    }

    private string BuildPath(string key) => _location + "/" + key + FileExtension;
}