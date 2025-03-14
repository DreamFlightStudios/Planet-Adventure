public interface IStorable<T>
{
    string Id { get; }
    void SetData(T data);
    T GetData();
}