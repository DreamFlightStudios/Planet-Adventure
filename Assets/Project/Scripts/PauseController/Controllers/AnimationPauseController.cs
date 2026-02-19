using UnityEngine;

public class AnimationPauseController : PauseController
{
    private Animator _animator;
    private float _savedSpeed;

    private void Awake() 
        => _animator = GetComponent<Animator>();

    protected override void OnGamePaused()
    {
        base.OnGamePaused();

        _savedSpeed = _animator.speed;
        _animator.speed = 0f;
    }

    protected override void OnGameResumed()
    {
        base.OnGameResumed();
        _animator.speed = _savedSpeed;
    }
}