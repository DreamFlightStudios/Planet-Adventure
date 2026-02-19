using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PauseController))]
public class Speaker : MonoBehaviour, IPausable
{
    [field: SerializeField] public SpeakerInfo Info { get; private set; }
    public bool IsPause { get; private set; }

    private AudioSource _source;
    private bool _wasPlaying;
    private float _savedTime;

    public event Action<PhraseInfo> StartSpeaked;
    public event Action<PhraseInfo> StopSpeaked;

    private void Awake() 
        => _source = GetComponent<AudioSource>();

    public void StartSpeak(PhraseInfo phrase)
    {
        if (_source.isPlaying)
            InterruptSpeak(phrase);

        _source.PlayOneShot(phrase.AudioClip);
        StartSpeaked?.Invoke(phrase);
    }

    public void InterruptSpeak(PhraseInfo phrase)
    {
        _source.Stop();
        StopSpeaked?.Invoke(phrase);
    }

    public void OnPause()
    {
        _wasPlaying = _source.isPlaying;

        if (_wasPlaying)
        {
            _savedTime = _source.time;
            _source.Pause();
        }
    }

    public void OnResume()
    {
        if (_wasPlaying)
        {
            _source.time = _savedTime;
            _source.UnPause();
        }
    }
}