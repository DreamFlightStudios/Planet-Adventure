using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : RootUI
{
    [SerializeField] private Image _background;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    public override void SwitchState(bool state)
    {
        if (state)
            _background.DOFade(1.0f, _fadeInDuration);
        else
            _background.DOFade(0.0f, _fadeOutDuration);
    }
}