using UnityEngine;

public class AudioPauseController : PauseController
{
    private AudioSource _audioSource;
    private bool _wasPlaying;
    private float _savedTime;

    private void Awake() 
        => _audioSource = GetComponent<AudioSource>();

    protected override void OnGamePaused()
    {
        base.OnGamePaused();
        _wasPlaying = _audioSource.isPlaying;

        if (_wasPlaying)
        {
            _savedTime = _audioSource.time;
            _audioSource.Pause();
        }
    }

    protected override void OnGameResumed()
    {
        base.OnGameResumed();

        if (_wasPlaying)
        {
            _audioSource.time = _savedTime;
            _audioSource.UnPause();
        }
    }
}