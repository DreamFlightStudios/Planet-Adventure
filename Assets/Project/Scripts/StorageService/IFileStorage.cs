public interface IFileStorage
{
    bool Exists(string fileName);
    string ReadText(string fileName);
    void WriteText(string fileName, string content);
    bool Delete(string fileName);
}
