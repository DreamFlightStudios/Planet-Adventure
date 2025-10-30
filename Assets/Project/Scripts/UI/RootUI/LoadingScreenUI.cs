using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private float _fadeDuration;

    public void OnLoadStarted()
    {
        _background.DOFade(1.0f, _fadeDuration);
    }

    public void OnLoadFinished()
    {
        _background.DOFade(0.0f, _fadeDuration);
    }
}