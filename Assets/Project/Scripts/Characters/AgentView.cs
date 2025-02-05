using UnityEngine;

[RequireComponent (typeof(Animator))]
public class AgentView : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void SetFloatParametrValue(AgentAnimationKey key, float value) 
        => _animator.SetFloat(key.ToString(), value);

    public void SetBoolParametrValue(AgentAnimationKey key, bool state) 
        => _animator.SetBool(key.ToString(), state);
}

public enum AgentAnimationKey
{
    Walking,
    Sprinting,
    Crouching,
    Falling,
    MovementVelocity,
}