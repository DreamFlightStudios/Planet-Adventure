using DG.Tweening;
using UnityEngine;

public class Door : InteractiveObject
{
    [Header("Transform")]
    [SerializeField] private Transform _openPosition;
    [SerializeField] private Transform _closePosition;

    [Header("Setting's")]
    [SerializeField] private float _closeOpenDuration;
    private bool _isOpen;

    public override void Interaction()
    {
        _isOpen = !_isOpen;
        var finalPosition = _isOpen ? _openPosition : _closePosition;
        transform.DOMove(finalPosition.position, _closeOpenDuration);

        base.Interaction();
    }
}