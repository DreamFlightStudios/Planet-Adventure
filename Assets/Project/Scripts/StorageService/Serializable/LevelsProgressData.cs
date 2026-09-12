using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

[Serializable]
[Preserve]
public class LevelsProgressData : SaveData
{
    public const int CurrentVersion = 1;

    public List<string> CompletedLevels = new();
}
