using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressService : ILevelProgressService
{
    private readonly ISaveRepository<LevelsProgressData> _repository;
    private readonly LevelsConfig _configuration;

    public event Action ProgressChanged;

    public IReadOnlyList<LevelInfo> Levels => _configuration.Levels;

    public LevelProgressService(ISaveRepository<LevelsProgressData> repository, LevelsConfig configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public void Initialize() => _repository.Load();

    public bool IsCompleted(string levelId) => _repository.Data.CompletedLevels.Contains(levelId);

    public bool IsUnlocked(string levelId)
    {
        int index = _configuration.IndexOf(levelId);

        if (index < 0)
            return false;

        if (_configuration.Levels[index].IsUnlockedByDefault || IsCompleted(levelId))
            return true;

        return index > 0 && IsCompleted(_configuration.Levels[index - 1].LevelId);
    }

    public void CompleteLevel(string levelId)
    {
        if (_configuration.IndexOf(levelId) < 0)
            Debug.LogWarning($"Level '{levelId}' is missing in {nameof(LevelsConfig)}. Progress is recorded, but the level will not appear in the menu.");

        if (IsCompleted(levelId))
            return;

        _repository.Data.CompletedLevels.Add(levelId);
        _repository.Save();

        ProgressChanged?.Invoke();
    }

    public bool TryGetNextLevelId(string levelId, out string nextLevelId)
    {
        nextLevelId = null;

        int index = _configuration.IndexOf(levelId);

        if (index < 0 || index + 1 >= _configuration.Levels.Length)
            return false;

        nextLevelId = _configuration.Levels[index + 1].LevelId;
        return true;
    }

    public void ResetProgress()
    {
        _repository.Reset();
        ProgressChanged?.Invoke();
    }
}
