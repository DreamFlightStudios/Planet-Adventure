public interface IReadonlyAgentStateMachineData
{
    IReadonlyAgentStateData CurrentStateData { get; }
    bool IsTransitioning { get; }
}