using TMPro;
using UnityEngine;

public class InteractionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private TMP_Text _contextField;

    [SerializeField] private Hand _interactor;

    private void Awake()
    {
        _interactor.ObjectDetected += OnInteractionDetected;
        _interactor.Interacted += OnInteractCompleted;

        _indicator.SetActive(false);
    }

    public void OnInteractionDetected(bool isEnter, string context)
    {
        _contextField.text = context;
        _indicator.SetActive(isEnter);
    }

    public void OnInteractCompleted() => _indicator.SetActive(false);
}