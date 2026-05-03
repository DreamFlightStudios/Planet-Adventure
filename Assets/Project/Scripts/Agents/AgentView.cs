using UnityEngine;

public class AgentView : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _hips;
    private Animator _animator;

    private void Awake() 
        => _animator = GetComponent<Animator>();

    public void SetFloatParametrValue(AgentAnimationKey key, float value) 
        => _animator.SetFloat(key.ToString(), value);

    public void SetBoolParametrValue(AgentAnimationKey key, bool state) 
        => _animator.SetBool(key.ToString(), state);

    public void InvokeTrigger(AgentAnimationKey key) 
        => _animator.SetTrigger(key.ToString());

    public void SwitchRagDoll(bool state)
    {
        _animator.enabled = false;

        foreach (var hip in _hips)
        {
            hip.isKinematic = !state;
        }
    }
}