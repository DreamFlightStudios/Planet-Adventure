using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker Info", menuName = "Configs/Dialogue/Create Speaker Info")]
public class SpeakerInfo : ScriptableObject
{
    [field: SerializeField] public string Name;
}