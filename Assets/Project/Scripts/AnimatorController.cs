using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private AnimationObject _controller;
    [SerializeField] private Animator _animator;

    private void OnMove(float forvardVelocity, float velocityY)
    {
        _animator.SetFloat("ForvardVelocity", forvardVelocity);
        _animator.SetFloat("VelocityY", velocityY);
    }

    private void OnEnable()
    {
        _controller.Moved += OnMove;
    }

    private void OnDisable()
    {
        _controller.Moved -= OnMove;
    }
}
