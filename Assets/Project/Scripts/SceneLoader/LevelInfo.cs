using System;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    [field: SerializeField] public string LevelId { get; private set; }
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public bool IsUnlockedByDefault { get; private set; }
}
