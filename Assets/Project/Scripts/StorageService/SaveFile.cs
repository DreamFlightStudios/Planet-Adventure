using System;
using UnityEngine;

public class SaveFile<TData> : ISaveRepository<TData> where TData : SaveData
{
    private const string BackupSuffix = "_backup";

    private readonly string _fileName;
    private readonly int _currentVersion;
    private readonly IFileStorage _storage;
    private readonly ISaveSerializer _serializer;
    private readonly Func<TData> _defaultFactory;

    public TData Data { get; private set; }

    public SaveFile(string fileName, int currentVersion, IFileStorage storage, ISaveSerializer serializer, Func<TData> defaultFactory)
    {
        _fileName = fileName;
        _currentVersion = currentVersion;
        _storage = storage;
        _serializer = serializer;
        _defaultFactory = defaultFactory;
    }

    public void Load()
    {
        string content = _storage.ReadText(_fileName);

        if (string.IsNullOrEmpty(content))
        {
            Reset();
            return;
        }

        Data = _defaultFactory.Invoke();

        try
        {
            _serializer.Populate(content, Data);
        }
        catch (Exception e)
        {
            Debug.LogError($"Save file '{_fileName}' is corrupted and was reset to defaults.  Error: {e}");

            Backup(content);
            Reset();
            return;
        }

        ResolveVersion(content);
    }

    public void Save()
    {
        Data.Version = _currentVersion;
        _storage.WriteText(_fileName, _serializer.Serialize(Data));
    }

    public void Reset()
    {
        Data = _defaultFactory.Invoke();
        Save();
    }

    private void ResolveVersion(string content)
    {
        if (Data.Version == _currentVersion)
            return;

        if (Data.Version > _currentVersion)
        {
            Debug.LogError($"Save file '{_fileName}' has version {Data.Version} newer than supported {_currentVersion}. It was backed up and reset to defaults.");

            Backup(content);
            Reset();
            return;
        }

        Debug.LogWarning($"Save file '{_fileName}' has version {Data.Version} and was upgraded to {_currentVersion}.");
        Save();
    }

    private void Backup(string content) => _storage.WriteText(_fileName + BackupSuffix, content);
}
