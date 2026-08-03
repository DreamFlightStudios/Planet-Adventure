using UnityEngine;

public class AgentCrouchingState : AgentGroundedState
{
    [SerializeField] private CapsuleCollider _defaultCollider;
    [SerializeField] private CapsuleCollider _crouchCollider;

    public override void Enter()
    {
        base.Enter();

        StateMachineData.SetCurrentSpeed(Config.CrouchingSpeed);

        _defaultCollider.enabled = false; 
        _crouchCollider.enabled = true;

        View.SetBoolParametrValue(AgentAnimationKey.Crouching, true);
    }

    public override void Exit()
    {
        base.Exit();

        _defaultCollider.enabled = true;
        _crouchCollider.enabled = false;

        View.SetBoolParametrValue(AgentAnimationKey.Crouching, false);
    }
}