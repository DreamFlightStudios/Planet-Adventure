using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PauseController))]
public class WarningIndicator : UIScreen, IPausable
{
    public bool IsPause { get; private set; }

    [SerializeField] private TMP_Text _indicator;
    private string _taskInfo;

    [SerializeField] private float _duration;
    [SerializeField] private float _fadeDuration;

    private InputSystem _input;
    private Coroutine _coroutine;
    private float _remainingTime;
    private bool _isShowing;

    private void Start() 
        => _indicator.DOFade(0.0f, 0.0f);

    public void Initialize(InputSystem input)
    {
        _input = input;
        _input.UI.QuestInfo.performed += ShowCurrentTaskInfo;
    }

    public void SendTaskInfo(string message)
    {
        _taskInfo = message;
        Send(message);
    }

    public void OnPause()
    {
        IsPause = true;

        if (_isShowing) 
            _indicator.DOPause();
    }

    public void OnResume()
    {
        IsPause = false;

        if (!_isShowing) 
            return;

        _indicator.DOPlay();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ShowWithRemainingTime());
        }
    }

    public void Send(string message)
    {
        if (_coroutine != null)
            return;

        _indicator.text = message;
        _coroutine = StartCoroutine(Show());
    }

    private void ShowCurrentTaskInfo(InputAction.CallbackContext context)
    {
        _indicator.text = _taskInfo;

        if (_coroutine == null && IsPause == false) 
            _coroutine = StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        _isShowing = true;
        _remainingTime = _duration + _fadeDuration;
        _indicator.DOFade(1, _fadeDuration);

        float timer = 0;
        while (timer < _duration + _fadeDuration)
        {
            if (IsPause)
            {
                _remainingTime -= timer;
                yield break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _indicator.DOFade(0, _fadeDuration);
        _coroutine = null;
        _isShowing = false;
    }

    private IEnumerator ShowWithRemainingTime()
    {
        _indicator.DOFade(1, 0);

        float timer = 0;
        while (timer < _remainingTime)
        {
            if (IsPause)
            {
                _remainingTime -= timer;
                yield break;
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        _indicator.DOFade(0, _fadeDuration);
        _coroutine = null;
        _isShowing = false;
    }

    private void OnDestroy()
    {
        if (_input != null) _input.UI.QuestInfo.performed -= ShowCurrentTaskInfo;
        _indicator.DOKill();
    }
}