using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SubtilesUI : AttachableContainerUI
{
    [SerializeField] private SubtitlesField _fieldPrefab;
    [SerializeField] private Transform _container;

    private Dictionary<PhraseInfo, SubtitlesField> _subtitles = new();

    public void OnSpeakStarted(PhraseInfo phrase)
    {
        var field = Instantiate(_fieldPrefab, _container);
        field.Initialize(phrase);

        _subtitles.Add(phrase, field);
    }

    public void OnSpeakCompleted(PhraseInfo phrase)
    {
        if (_subtitles.ContainsKey(phrase))
        {
            Destroy(_subtitles[phrase].gameObject);
            _subtitles.Remove(phrase);
        }
    }
    protected override void OnConfigurated(UserData data) 
        => _container.gameObject.SetActive(data.SettingsData.Subtitles);
}