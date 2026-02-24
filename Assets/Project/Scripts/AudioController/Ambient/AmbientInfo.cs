using UnityEngine;

[CreateAssetMenu(fileName = "New Ambient Info", menuName = "Configs/Gameplay/Create Ambient Info")]
public class AmbientInfo : ScriptableObject
{
    [SerializeField] private IntRange _startDelayRange;
    [SerializeField] private IntRange _delayRange;
    [SerializeField] private AudioClip[] _clips;

    public AudioClip GetRandomClip() 
        => _clips[Random.Range(0, _clips.Length)];

    public int GetStartRandomDelay()
    => Random.Range(_startDelayRange.Min, _startDelayRange.Max);

    public int GetRandomDelay() 
        => Random.Range(_delayRange.Min, _delayRange.Max);
}