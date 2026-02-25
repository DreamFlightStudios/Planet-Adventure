using DG.Tweening;
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

    public void Play(AudioClip clip, SourceType type, float fadeDuration = 0.0f)
    {
        var source = GetSource(type);
        
        if (source.isPlaying)
            CrossFade(clip, type, fadeDuration);
        else
            source.PlayOneShot(clip);
    }

    public void Stop(SourceType type, float fadeDuration = 0.0f)
    {
        var source = GetSource(type);

        if (source.isPlaying == false)
            return;

        source.DOFade(0, fadeDuration).OnComplete(() =>
        {
            source.Stop();
            source.volume = 1.0f;
        });
    }

    public void CrossFade(AudioClip newClip, SourceType type, float fadeDuration = 0.0f)
    {
        var source = GetSource(type);

        source.DOFade(0, fadeDuration / 2.0f).OnComplete(() =>
        {
            source.clip = newClip;
            source.Play();
            source.DOFade(1.0f, fadeDuration / 2.0f);
        });
    }

    public void StopAll(float fadeDuration = 0.0f)
    {
        Stop(SourceType.Ambient, fadeDuration);
        Stop(SourceType.Interaction, fadeDuration);
        Stop(SourceType.Music, fadeDuration);
        Stop(SourceType.UI, fadeDuration);
        Stop(SourceType.Default, fadeDuration);
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
}