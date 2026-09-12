using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private GameObject _lockIndicator;

    public string LevelId { get; private set; }
    public event Action<string> ButtonPressed;

    public void Initialize(LevelInfo info, bool isUnlocked)
    {
        LevelId = info.LevelId;

        _label.text = string.IsNullOrEmpty(info.DisplayName) ? info.LevelId : info.DisplayName;
        _button.interactable = isUnlocked;

        if (_lockIndicator != null)
            _lockIndicator.SetActive(isUnlocked == false);
    }

    public void OnPressed() => ButtonPressed?.Invoke(LevelId);
}
