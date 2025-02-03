using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly AgentController Agent;
    protected readonly StateMachineData Data;
    protected readonly IStateSwitcher StateSwitcher;

    protected bool IsGrounded => Agent.GroundChecker.IsGrounded;
    protected IAgentMovementInput Input => Agent.Input;
    protected Rigidbody CharacterController => Agent.CharacterController;
    protected Transform Transform => Agent.gameObject.transform;
    protected AgentConfig Config => Agent.Config;
    protected AgentView View => Agent.View;
    protected AgentRoatationStrategy RotationStrategy => Agent.RotationStrategy;

    private float _smoothRotationVelocity;
    private float _targetRotation;

    public MovementState(IStateSwitcher switcher, StateMachineData data, AgentController agent)
    {
        Agent = agent;
        Data = data;
        StateSwitcher = switcher;
    }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void Update() 
        => MovementCalculation();

    private void MovementCalculation()
    {
        if (Input.MovementInput.sqrMagnitude > 0.0f)
        {
            _targetRotation = RotationStrategy.RotationCalculation(Input.MovementInput, Transform, 
                ref _smoothRotationVelocity, Config.RotationSpeed, _targetRotation);

            Vector3 direction = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            Data.Velocity = new Vector3(direction.x, 0.0f, direction.z);

            CharacterController.linearVelocity = Data.Velocity * Time.fixedDeltaTime * Data.Speed +
                new Vector3(0.0f, CharacterController.linearVelocity.y, 0.0f);
        }

        Data.SmoothInput = Vector2.Lerp(Data.SmoothInput, Input.MovementInput, Config.Acceleration * Time.deltaTime);
        View.SetInput(Data.SmoothInput.sqrMagnitude);
    }
}