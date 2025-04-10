using UnityEngine;

[CreateAssetMenu(fileName = "New Phrase Info", menuName = "Configs/Dialogue/Create Phrase Info")]
public class PhraseInfo : ScriptableObject
{
    [field: SerializeField] public SpeakerInfo SpeakerInfo;
    [field: SerializeField] public AudioClip AudioClip;
    [field: SerializeField] public string Text;
}