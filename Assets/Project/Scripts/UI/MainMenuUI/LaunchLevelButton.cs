using System;
using UnityEngine;

public class LaunchLevelButton : MonoBehaviour
{
    [field: SerializeField] public string LevelId {  get; private set; }
    public LevelData LevelData { get; private set; }
    public event Action<string> ButtonPressed;

    public void Initialize(bool isUnlocked = false)
    {
        gameObject.SetActive(isUnlocked);
        LevelData = new(LevelId, isUnlocked);
    }

    public void OnPressed() => ButtonPressed?.Invoke(LevelId);
}