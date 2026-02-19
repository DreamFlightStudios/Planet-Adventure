using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(PauseController))]
public class DialogueController : MonoBehaviour, IPausable
{
    public bool IsPause { get; private set; }

    [Header("Info")]
    [SerializeField] private Phrase[] _phrases;

    [Header("Event's")]
    [SerializeField] private UnityEvent Activated;
    [SerializeField] private UnityEvent Completed;

    [Header("Settings")]
    [SerializeField, Range(1f, 2f)] private float _perceptionRate = 1.2f;
    [SerializeField] private bool _activateOnStart;

    private SubtilesUI _subtitles;
    private Coroutine _coroutine;
    private Dictionary<string, Speaker> _speakers = new();

    [Inject]
    private void Construct(SubtilesUI subtitles) 
        => _subtitles = subtitles;

    private void Start()
    {
        FindSpeakersOnScene();
        SubscribeToSpeakers();

        if (_activateOnStart)
            Activate();
    }

    public void Activate()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CommunicateRoutine());
            Activated?.Invoke();
        }
    }

    public void Complete()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
            Completed?.Invoke();
        }
    }

    public void OnPause() => IsPause = true;

    public void OnResume() => IsPause = false;

    private IEnumerator CommunicateRoutine()
    {
        foreach (var phrase in _phrases)
        {
            while (IsPause)
                yield return null;

            var info = phrase.Info;

            if (!_speakers.ContainsKey(info.SpeakerInfo.Name))
            {
                Debug.LogError($"Speaker {info.SpeakerInfo.Name} not found!");
                continue;
            }

            var speaker = _speakers[info.SpeakerInfo.Name];

            if (info.AudioClip == null)
            {
                Debug.LogError($"AudioClip is null for phrase with speaker {info.SpeakerInfo.Name}!");
                continue;
            }

            speaker.StartSpeak(info);

            float duration = info.AudioClip.length * _perceptionRate;
            float timer = 0f;

            while (timer < duration)
            {
                if (IsPause)
                {
                    while (IsPause)
                        yield return null;
                }

                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            speaker.InterruptSpeak(info);
            timer = 0f;

            while (timer < phrase.Delay)
            {
                if (IsPause)
                {
                    while (IsPause)
                        yield return null;
                }

                timer += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        Complete();
    }

    private void FindSpeakersOnScene()
    {
        _speakers.Clear();
        var sceneSpeakers = FindObjectsByType<Speaker>(FindObjectsSortMode.None);

        foreach (var phrase in _phrases)
        {
            if (phrase.Info?.SpeakerInfo == null)
                continue;

            var speakerName = phrase.Info.SpeakerInfo.Name;

            if (string.IsNullOrEmpty(speakerName))
                continue;

            if (!_speakers.ContainsKey(speakerName))
            {
                foreach (var speaker in sceneSpeakers)
                {
                    if (speaker != null && speaker.Info != null && speaker.Info.Name == speakerName)
                    {
                        _speakers.Add(speakerName, speaker);
                        break;
                    }
                }
            }
        }
    }

    private void SubscribeToSpeakers()
    {
        if (_subtitles == null)
            return;

        foreach (var speaker in _speakers)
        {
            if (speaker.Value != null)
            {
                speaker.Value.StartSpeaked -= _subtitles.OnSpeakStarted;
                speaker.Value.StopSpeaked -= _subtitles.OnSpeakCompleted;

                speaker.Value.StartSpeaked += _subtitles.OnSpeakStarted;
                speaker.Value.StopSpeaked += _subtitles.OnSpeakCompleted;
            }
        }
    }

    private void OnDestroy()
    {
        if (_subtitles != null)
        {
            foreach (var speaker in _speakers)
            {
                if (speaker.Value != null)
                {
                    speaker.Value.StartSpeaked -= _subtitles.OnSpeakStarted;
                    speaker.Value.StopSpeaked -= _subtitles.OnSpeakCompleted;
                }
            }
        }
    }
}