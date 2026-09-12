using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : UIScreen
{
    [SerializeField] private Image _background;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    public override void SwitchStateByContainer(bool state, GameObject container)
        => ApplyState(state);

    protected override void OnShow()
        => _background.DOFade(1.0f, _fadeInDuration);

    protected override void OnHide()
        => _background.DOFade(0.0f, _fadeOutDuration);
}
