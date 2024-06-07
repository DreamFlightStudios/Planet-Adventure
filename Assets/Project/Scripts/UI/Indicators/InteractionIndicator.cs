using UnityEngine;

public class InteractionIndicator : Indicator
{
    [SerializeField] private Hand _playerHand;

    private void OnObjectDetection(bool state)
    {
        EnableDisable(state);
    }

    private void OnInteracted() => EnableDisable(false);

    private void OnEnable()
    {
        _playerHand.ObjectDetected += OnObjectDetection;
        _playerHand.Interacted += OnInteracted;
    }

    private void OnDisable()
    {
        _playerHand.ObjectDetected -= OnObjectDetection;
        _playerHand.Interacted -= OnInteracted;
    }
} 
