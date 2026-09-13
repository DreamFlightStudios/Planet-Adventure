using System;

public interface IPauseService
{
    bool IsPaused { get; }

    event Action<bool> PauseChanged;

    void Pause(object owner);
    void Resume(object owner);
}
