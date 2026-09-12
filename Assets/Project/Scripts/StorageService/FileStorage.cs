using System;
using System.IO;
using System.Text;
using UnityEngine;

public class FileStorage : IFileStorage
{
    private const string FileExtension = ".json";
    private const string TempExtension = ".tmp";

    private readonly string _location;

    public FileStorage()
    {
        _location = Application.persistentDataPath + "/Saves";
        CreateLocation();
    }

    public bool Exists(string fileName) => File.Exists(BuildPath(fileName));

    public string ReadText(string fileName)
    {
        string path = BuildPath(fileName);

        if (File.Exists(path) == false)
            return null;

        try
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from: {path}.  Error: {e}");
            return null;
        }
    }

    public void WriteText(string fileName, string content)
    {
        string path = BuildPath(fileName);
        string tempPath = path + TempExtension;

        try
        {
            CreateLocation();

            File.WriteAllText(tempPath, content, Encoding.UTF8);
            ReplaceFile(tempPath, path);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save to: {path}.  Error: {e}");
        }
    }

    public bool Delete(string fileName)
    {
        string path = BuildPath(fileName);

        if (File.Exists(path) == false)
            return false;

        try
        {
            File.Delete(path);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to delete: {path}.  Error: {e}");
            return false;
        }
    }

    private void CreateLocation()
    {
        try
        {
            Directory.CreateDirectory(_location);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to create saves directory: {_location}.  Error: {e}");
        }
    }

    private void ReplaceFile(string tempPath, string path)
    {
        if (File.Exists(path) == false)
        {
            File.Move(tempPath, path);
            return;
        }

        try
        {
            File.Replace(tempPath, path, null);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"File.Replace is unsupported for: {path}. Falling back to delete and move.  Error: {e}");

            File.Delete(path);
            File.Move(tempPath, path);
        }
    }

    private string BuildPath(string fileName) => _location + "/" + fileName + FileExtension;
}
