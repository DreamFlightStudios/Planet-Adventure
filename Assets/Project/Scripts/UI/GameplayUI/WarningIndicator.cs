using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarningIndicator : UIScreen
{
    [SerializeField] private TMP_Text _indicator;
    private string _taskInfo;

    [SerializeField] private float _duration;
    [SerializeField] private float _fadeDuration;

    private InputSystem _input;
    private Coroutine _coroutine;

    private void Start()
        => _indicator.DOFade(0.0f, 0.0f);

    public void Initialize(InputSystem input)
    {
        _input = input;
        _input.Player.QuestInfo.performed += ShowCurrentTaskInfo;
    }

    public void SendTaskInfo(string message)
    {
        _taskInfo = message;
        Send(message);
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

        if (_coroutine == null)
            _coroutine = StartCoroutine(Show());
    }

    // The tween and the wait both run on scaled time, so the indicator freezes with the game on pause.
    private IEnumerator Show()
    {
        _indicator.DOFade(1, _fadeDuration);

        yield return new WaitForSeconds(_duration + _fadeDuration);

        _indicator.DOFade(0, _fadeDuration);
        _coroutine = null;
    }

    private void OnDestroy()
    {
        if (_input != null) _input.Player.QuestInfo.performed -= ShowCurrentTaskInfo;
        _indicator.DOKill();
    }
}
