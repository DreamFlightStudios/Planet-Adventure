using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarningIndicator : AttachableContainerUI
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
        _input.UI.QuestInfo.performed += ShowCurrentTaskInfo;
    }

    public void SendTaskInfo(string massage)
    {
        _taskInfo = massage;
        Send(massage);
    }

    public void Send(string massage)
    {
        if (_coroutine == null)
        {
            _indicator.text = massage;
            _coroutine = StartCoroutine(Show());
        }
    }

    private void ShowCurrentTaskInfo(InputAction.CallbackContext context)
    {
        _indicator.text = _taskInfo;

        if (_coroutine == null)
            _coroutine = StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        _indicator.DOFade(1.0f, _fadeDuration);

        yield return 
            new WaitForSecondsRealtime(_duration + _fadeDuration);

        _indicator.DOFade(0.0f, _fadeDuration);
        _coroutine = null;
    }

    private void OnDestroy()
        => _input.UI.QuestInfo.performed -= ShowCurrentTaskInfo;
}