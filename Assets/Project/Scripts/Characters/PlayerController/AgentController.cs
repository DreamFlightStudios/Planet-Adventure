using UnityEngine;
public class AgentController : MonoBehaviour
{
    [field: SerializeField] public AgentView View { get; private set; }
    [field: SerializeField] public AgentConfig Config { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }

    public AgentRoatationStrategy RotationStrategy { get; private set; }
    public IAgentMovementInput Input { get; private set; }
    public Rigidbody CharacterController { get; private set; }

    private StateMachine _stateMachine;

    private void Awake()
    {
        CharacterController = GetComponent<Rigidbody>();
        RotationStrategy = GetComponent<AgentRoatationStrategy>();
        Input = GetComponent<IAgentMovementInput>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        StateMachineData data = new();

        _stateMachine.AddState(new FallingState(_stateMachine, data, this));
        _stateMachine.AddState(new WalkingState(_stateMachine, data, this));
        _stateMachine.AddState(new JumpingState(_stateMachine, data, this));
        _stateMachine.AddState(new SprintingState(_stateMachine, data, this));
        _stateMachine.AddState(new CrouchingState(_stateMachine, data, this));

        _stateMachine.Initialize();
    }

    private void FixedUpdate() => _stateMachine.Update();
}