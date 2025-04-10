using TMPro;
using UnityEngine;

public class SubtitlesField : MonoBehaviour
{
    [SerializeField] private TMP_Text _field;

    public void Initialize(PhraseInfo phrase) 
        => _field.text = $"{phrase.SpeakerInfo.Name}: {phrase.Text}";
}