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

    // Unscaled: the scene change can be requested from the pause menu, where the game is frozen.
    protected override void OnShow()
        => _background.DOFade(1.0f, _fadeInDuration).SetUpdate(true);

    protected override void OnHide()
        => _background.DOFade(0.0f, _fadeOutDuration).SetUpdate(true);
}
