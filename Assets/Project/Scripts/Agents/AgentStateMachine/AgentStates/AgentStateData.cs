public class AgentStateData : IReadonlyAgentStateData
{
    public string StateName { get; set; }

    public AgentStateData(string stateName)
    {
        StateName = stateName;
    }
}