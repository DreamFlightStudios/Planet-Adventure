using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Camera Config", menuName = "Configs/Gameplay/Create Gameplay Camera Config")]
public class GameplayCameraConfig : ScriptableObject
{
    [field: SerializeField] public float Sensivity { get; private set; }

    [field: SerializeField] public Vector2Int _minMaxRotationX { get; private set; }
}
