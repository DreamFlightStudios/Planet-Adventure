public interface ISaveRepository<TData> where TData : SaveData
{
    TData Data { get; }

    void Load();
    void Save();
    void Reset();
}
