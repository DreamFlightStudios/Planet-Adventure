using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _fullInventoryWarning;

    [SerializeField] private float _fadeSpeed;
    [SerializeField] private float _fadePause;

    public void InvokeWarning(string message, WarningType type)
    {
        _warningText.text = message;

        switch (type)
        {
            case WarningType.FullInventory :
                StartCoroutine(FadeText());
                _audioManager.PlaySound(_fullInventoryWarning, SoundType.Dispatcher);
                break;

            case WarningType.NewItem :
                StartCoroutine(FadeText());
                break;
        }
    }

    private IEnumerator FadeText()
    {
        _warningText.DOFade(1, _fadeSpeed);
        yield return new WaitForSecondsRealtime(_fadePause);

        _warningText.DOFade(0, _fadeSpeed);
    }
}

public enum WarningType
{
    FullInventory,
    NewItem
}
