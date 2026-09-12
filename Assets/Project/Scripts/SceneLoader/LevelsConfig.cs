using UnityEngine;

[CreateAssetMenu(fileName = "New Levels Config", menuName = "Configs/System/Create Levels Config")]
public class LevelsConfig : ScriptableObject
{
    [Header("Levels")]
    [field: SerializeField] public LevelInfo[] Levels { get; private set; }

    [Header("System Scenes")]
    [field: SerializeField] public string MainMenuSceneName { get; private set; }

    public int IndexOf(string levelId)
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].LevelId == levelId)
                return i;
        }

        return -1;
    }

    public LevelInfo GetById(string levelId)
    {
        int index = IndexOf(levelId);

        return index < 0 ? null : Levels[index];
    }
}
