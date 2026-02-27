using UnityEngine;

[CreateAssetMenu(fileName = "New Ambient Info", menuName = "Configs/Gameplay/Create Ambient Info")]
public class AmbientInfo : ScriptableObject
{
    [SerializeField] private IntRange _startDelayRange;
    [SerializeField] private IntRange _delayRange;
    [SerializeField] private AmbientItem[] _ambient;

    public AmbientItem GetRandomAmbientItem() 
        => _ambient[Random.Range(0, _ambient.Length)];

    public float GetStartRandomDelay()
        => Random.Range(_startDelayRange.Min, _startDelayRange.Max);

    public float GetRandomDelayFromAmbientItem(AmbientItem item) 
        => Random.Range(_delayRange.Min, _delayRange.Max) + item.Clip.length;
}

[System.Serializable]
public struct AmbientItem
{
    [field: SerializeField] public AudioClip Clip {  get; private set; }
    [field: SerializeField] public float FadeInDelay { get; private set; }
    [field: SerializeField] public float FadeOutDelay { get; private set; }
}