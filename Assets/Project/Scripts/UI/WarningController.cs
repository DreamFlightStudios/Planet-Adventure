using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private AudioClip _fullInventoryWarning;

    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private float _fadeSpeed;
    [SerializeField] private float _fadeAwait;

    public void InvokeWarning(string massage, WarningType type)
    {
        _warningText.text = massage;

        switch (type)
        {
            case WarningType.FullInventory :
                StartCoroutine(FadeText());
                _audioManager.PlaySound(_fullInventoryWarning, SoundType.Dispatcher);
                break;
        }
    }

    private IEnumerator FadeText()
    {
        _warningText.DOFade(1, _fadeSpeed);
        yield return new WaitForSecondsRealtime(_fadeAwait);

        _warningText.DOFade(0, _fadeSpeed);
        StopCoroutine(FadeText());
    }
}

public enum WarningType
{
    FullInventory
}
