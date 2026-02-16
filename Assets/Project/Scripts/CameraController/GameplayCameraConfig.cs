using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Camera Config", menuName = "Configs/Gameplay/Create Gameplay Camera Config")]
public class GameplayCameraConfig : ScriptableObject
{
    [field: SerializeField] public Vector2Int MinMaxRotationX { get; private set; }
}