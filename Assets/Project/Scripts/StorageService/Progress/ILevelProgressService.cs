using System;
using System.Collections.Generic;

public interface ILevelProgressService
{
    IReadOnlyList<LevelInfo> Levels { get; }

    event Action ProgressChanged;

    bool IsCompleted(string levelId);
    bool IsUnlocked(string levelId);
    void CompleteLevel(string levelId);
    bool TryGetNextLevelId(string levelId, out string nextLevelId);
    void ResetProgress();
}
