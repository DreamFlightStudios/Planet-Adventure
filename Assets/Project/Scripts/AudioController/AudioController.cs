using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : Configurable
{
    [Header("Sources")]
    [SerializeField] private AudioSource _ambient;
    [SerializeField] private AudioSource _interaction;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _ui;
    [SerializeField] private AudioSource _default;

    [Header("Audio Mixer Setup")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _ambientMixerKey;
    [SerializeField] private string _dialoguesMixerKey;
    [SerializeField] private string _interfaceMixerKey;
    [SerializeField] private string _environmentMixerKey;
    [SerializeField] private string _musicMixerKey;

    private Dictionary<SourceType, Tween> _activeFades = new Dictionary<SourceType, Tween>();

    public void Play(AudioClip clip, SourceType type, float fadeDuration = 0f)
    {
        if (clip == null)
        {
            Debug.LogWarning($"Attempted to play null clip on {type}");
            return;
        }

        var source = GetSource(type);
        if (source == null)
        {
            Debug.LogWarning($"No AudioSource found for type: {type}");
            return;
        }

        KillActiveFade(type);

        if (source.isPlaying && fadeDuration > 0)
        {
            CrossFade(clip, type, fadeDuration);
            return;
        }

        source.clip = clip;
        source.volume = fadeDuration > 0 ? 0f : 1f;
        source.Play();

        if (fadeDuration > 0)
        {
            var fade = source.DOFade(1f, fadeDuration)
                .SetId($"AudioFade_{type}")
                .OnComplete(() => _activeFades.Remove(type));
            _activeFades[type] = fade;
        }
    }

    public void Stop(SourceType type, float fadeDuration = 0f)
    {
        var source = GetSource(type);
        if (source == null || source.isPlaying == false)
            return;

        KillActiveFade(type);

        if (fadeDuration <= 0)
        {
            source.Stop();
            return;
        }

        var fade = source.DOFade(0f, fadeDuration)
            .SetId($"AudioFade_{type}")
            .OnComplete(() =>
            {
                source.Stop();
                source.volume = 1f;
                _activeFades.Remove(type);
            });

        _activeFades[type] = fade;
    }

    private void CrossFade(AudioClip newClip, SourceType type, float fadeDuration)
    {
        var source = GetSource(type);
        var halfFade = fadeDuration / 2f;

        var fadeOut = source.DOFade(0, halfFade)
            .SetId($"AudioFade_{type}_out")
            .OnComplete(() =>
            {
                source.clip = newClip;
                source.volume = 0f;
                source.Play();

                var fadeIn = source.DOFade(1f, halfFade)
                    .SetId($"AudioFade_{type}_in")
                    .OnComplete(() => _activeFades.Remove(type));
                _activeFades[type] = fadeIn;
            });

        _activeFades[type] = fadeOut;
    }

    public void StopAll(float fadeDuration = 0f)
    {
        foreach (SourceType type in Enum.GetValues(typeof(SourceType)))
        {
            Stop(type, fadeDuration);
        }
    }

    protected override void OnConfigurated(UserData data)
    {
        base.OnConfigurated(data);
        var settingsData = data.SettingsData;

        _mixer.SetFloat(_ambientMixerKey, settingsData.Ambient);
        _mixer.SetFloat(_dialoguesMixerKey, settingsData.Dialogues);
        _mixer.SetFloat(_interfaceMixerKey, settingsData.Interface);
        _mixer.SetFloat(_environmentMixerKey, settingsData.Environment);
        _mixer.SetFloat(_musicMixerKey, settingsData.Music);
    }

    private AudioSource GetSource(SourceType type)
    {
        return type switch
        {
            SourceType.Ambient => _ambient,
            SourceType.Music => _music,
            SourceType.UI => _ui,
            SourceType.Interaction => _interaction,
            _ => _default
        };
    }

    private void KillActiveFade(SourceType type)
    {
        if (_activeFades.TryGetValue(type, out var tween) && tween.IsActive())
        {
            tween.Kill();
            _activeFades.Remove(type);
        }
    }

    private void OnDestroy()
    {
        foreach (var tween in _activeFades.Values)
        {
            if (tween.IsActive())
                tween.Kill();
        }

        _activeFades.Clear();
    }
}