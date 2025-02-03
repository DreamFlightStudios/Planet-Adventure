using UnityEngine;

public class AgentView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private string _movementInputKey;
    [SerializeField] private string _runningKey;
    [SerializeField] private string _fallingKey;
    [SerializeField] private string _crouchingKey;

    public void SetInput(float input) => _animator.SetFloat(_movementInputKey, input);

    public void OnRunning(bool isRunning) => _animator.SetBool(_runningKey, isRunning);

    public void OnFalling(bool IsFalling) => _animator.SetBool(_fallingKey, IsFalling);

    public void OnCrouching(bool IsCrouching) => _animator.SetBool(_crouchingKey, IsCrouching);
}