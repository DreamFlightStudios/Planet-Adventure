using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePauseController : PauseController
{
    private ParticleSystem _particleSystem;
    private bool _wasPlaying;

    private void Awake() 
        => _particleSystem = GetComponent<ParticleSystem>();

    protected override void OnGamePaused()
    {
        base.OnGamePaused();
        _wasPlaying = _particleSystem.isPlaying;

        if (_wasPlaying)
        {
            _particleSystem.Pause();
        }
    }

    protected override void OnGameResumed()
    {
        base.OnGameResumed();

        if (_wasPlaying)
        {
            _particleSystem.Play();
        }
    }
}