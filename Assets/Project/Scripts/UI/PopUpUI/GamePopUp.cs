using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePopUp : MonoBehaviour
{
    [field: SerializeField] public Button CancelButton;
    [field: SerializeField] public Button ApplyButton;

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;

    public void Initialize(string title, string description, UnityAction applyAction, UnityAction cancelAction)
    {
        _title.text = title;
        _description.text = description;

        ApplyButton.onClick.AddListener(applyAction);
        CancelButton.onClick.AddListener(cancelAction);
    }

    private void OnDestroy()
    {
        CancelButton.onClick.RemoveAllListeners();
        ApplyButton.onClick.RemoveAllListeners();
    }
}