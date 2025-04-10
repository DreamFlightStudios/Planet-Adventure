using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DialogueHandler : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Phrase[] _phrases;

    [Header("Event's")]
    [SerializeField] private UnityEvent Activated;
    [SerializeField] private UnityEvent Completed;

    [Header("Setting's")]
    [SerializeField, Range(1f, 2f)] private float _informationPerceptionRateCoefficient = 1.2f;
    [SerializeField] private bool _activateOnStart;

    private Dictionary<string, Speaker> _speakers = new();
    private SubtilesUI _subtitles;
    private Coroutine _coroutine;

    [Inject]
    private void Construct(SubtilesUI subtiles)
    {
        FindSpeakersOnScene();
        _subtitles = subtiles;

        foreach (var speaker in _speakers)
        {
            speaker.Value.StartSpeaked += subtiles.OnSpeakStarted;
            speaker.Value.StopSpeaked += subtiles.OnSpeakCompleted;
        }
    }

    private void Start()
    {
        if (_activateOnStart)
            Activate();
    }

    public void Activate()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Ñommunicate());
            Activated?.Invoke();
        }
        else
            Debug.LogError($"Coroutine {_coroutine} already started");
    }

    public void Complete()
    {
        if (_coroutine != null)
        {
            StopCoroutine(Ñommunicate());
            _coroutine = null;

            Completed?.Invoke();
        }
        else
            Debug.LogError($"Coroutine {_coroutine} has a null value");
    }

    private IEnumerator Ñommunicate()
    {
        foreach (var phrase in _phrases)
        {
            var phraseInfo = phrase.Info;
            _speakers[phraseInfo.SpeakerInfo.Name].StartSpeak(phraseInfo);
            yield return new WaitForSecondsRealtime(phraseInfo.AudioClip.length * _informationPerceptionRateCoefficient);
            _speakers[phraseInfo.SpeakerInfo.Name].InterruptSpeak(phraseInfo);
            yield return new WaitForSecondsRealtime(phrase.Delay);
        }

        Completed?.Invoke();
    }

    private void FindSpeakersOnScene()
    {
        var sceneSpeakers = FindObjectsByType<Speaker>(FindObjectsSortMode.None);

        foreach (var phrase in _phrases)
        {
            var speakerName = phrase.Info.SpeakerInfo.Name;

            if (_speakers.ContainsKey(speakerName) == false)
            {
                foreach(var speaker in sceneSpeakers)
                {
                    if (speaker.Info.Name == speakerName)
                        _speakers.Add(speakerName, speaker);
                }
            }
            else
                return;
        }
    }

    private void OnDestroy()
    {
        foreach (var speaker in _speakers)
        {
            speaker.Value.StartSpeaked -= _subtitles.OnSpeakStarted;
            speaker.Value.StopSpeaked -= _subtitles.OnSpeakCompleted;
        }
    }
}