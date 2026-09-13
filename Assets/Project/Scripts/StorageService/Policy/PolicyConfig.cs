using UnityEngine;

[CreateAssetMenu(fileName = "New Policy Config", menuName = "Configs/System/Create Policy Config")]
public class PolicyConfig : ScriptableObject
{
    [Header("PopUp")]
    [field: SerializeField] public PopUpConfig PopUp { get; private set; }

    [Header("Version")]
    [field: SerializeField, Min(1)] public int Version { get; private set; } = 1;
}
