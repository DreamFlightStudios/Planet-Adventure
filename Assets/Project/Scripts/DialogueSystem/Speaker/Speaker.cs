using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Speaker : MonoBehaviour
{
    [field: SerializeField] public SpeakerInfo Info { get; private set; }

    private AudioSource _source;

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
}