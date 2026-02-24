using DG.Tweening;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _ambient;
    [SerializeField] private AudioSource _interaction;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _ui;
    [SerializeField] private AudioSource _default;

    [Header("Fade Settings")]
    [SerializeField] private float _defaultFadeDuration = 2f;
    [SerializeField] private Ease _fadeEase = Ease.OutQuad;

    public void Play(AudioClip clip, SourceType type)
    {
        var source = GetSource(type);
        source.PlayOneShot(clip);
    }

    public void Stop(SourceType type, float fadeDuration = -1f)
    {
        var source = GetSource(type);

        if (source == null || !source.isPlaying) 
            return;

        if (fadeDuration < 0)
            fadeDuration = _defaultFadeDuration;

        if (fadeDuration > 0)
        {
            StopWithFade(source, fadeDuration);
        }
        else
        {
            source.Stop();
            source.loop = false;
        }
    }

    public void StopAll(float fadeDuration = -1f)
    {
        Stop(SourceType.Ambient, fadeDuration);  
        Stop(SourceType.Music, fadeDuration);
        Stop(SourceType.UI, fadeDuration);
    }

    public void CrossFade(AudioClip newClip, SourceType type, float fadeDuration = -1f)
    {
        if (fadeDuration < 0)
            fadeDuration = _defaultFadeDuration;

        var source = GetSource(type);
        if (source == null) return;

        float originalVolume = source.volume;

        // Fade out current
        source.DOFade(0, fadeDuration / 2f).OnComplete(() =>
        {
            source.clip = newClip;
            source.Play();

            // Fade in new
            source.DOFade(originalVolume, fadeDuration / 2f).SetEase(_fadeEase);
        }).SetEase(_fadeEase);
    }

    private void StopWithFade(AudioSource source, float fadeDuration)
    {
        float originalVolume = source.volume;
        source.DOFade(0, fadeDuration).OnComplete(() =>
        {
            source.Stop();
            source.volume = originalVolume;
            source.loop = false;
        });
    }


    private AudioSource GetSource(SourceType type)
    {
        switch (type)
        {
            case SourceType.Ambient:
                return _ambient;
            case SourceType.Music:
                return _music;
            case SourceType.UI:
                return _ui;
            default:
                return _default;
        }
    }
}