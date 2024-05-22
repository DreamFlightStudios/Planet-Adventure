using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private IAnimated _controller;

    private void Awake() => _controller ??= GetComponent<IAnimated>();

    private void OnMove(float forvardVelocity)
    {
        _animator.SetFloat("ForvardVelocity", forvardVelocity);
    }
    private void OnRotate(float velocityY)
    {
        _animator.SetFloat("TurnVelocity", velocityY);
    }

    private void OnEnable()
    {
        if (_controller != null)
            _controller.Moved += OnMove;
    }

    private void OnDisable()
    {
        if (_controller != null)
            _controller.Moved -= OnMove;
    }
}
