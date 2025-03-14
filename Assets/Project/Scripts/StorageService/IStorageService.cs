using System.Collections.Generic;

public interface IStorageService
{
    void Save(string key, object data);
    T Load<T>(string key) where T : class;
    IEnumerable<string> GetAllSaves();
    bool FileExists(string key);
    bool Delete(string key);
}