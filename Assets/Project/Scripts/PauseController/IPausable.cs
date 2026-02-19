public interface IPausable
{
    public bool IsPause { get; }
    void OnPause();
    void OnResume();
}