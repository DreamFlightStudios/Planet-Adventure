public interface ISaveSerializer
{
    string Serialize(object data);
    void Populate<T>(string content, T target) where T : class;
}
