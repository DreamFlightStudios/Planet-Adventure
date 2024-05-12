using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private IAnimated _controller;

    private void Awake() => _controller ??= GetComponent<IAnimated>();

    private void OnMove(float forvardVelocity, float velocityY)
    {
        _animator.SetFloat("ForvardVelocity", forvardVelocity);
        _animator.SetFloat("VelocityY", velocityY);
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
