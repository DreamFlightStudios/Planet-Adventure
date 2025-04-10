using System;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    [field: SerializeField] public SpeakerInfo Info;
    [SerializeField] private AudioSource _source;

    public event Action<PhraseInfo> StartSpeaked;
    public event Action<PhraseInfo> StopSpeaked;

    public void StartSpeak(PhraseInfo phrase)
    {
        if (_source.isPlaying == true)
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