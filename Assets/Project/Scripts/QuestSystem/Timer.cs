using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : InteractiveObject
{
    [SerializeField] private UnityEvent<int> Counted;
    [SerializeField] private UnityEvent AccountOver;

    [SerializeField] private int _time;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;

    public void ActivateTimer()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Count());
        else
            Debug.LogError($"Coroutine {_coroutine} already started");
    }

    public void StopTimer()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        else
            Debug.LogError($"Coroutine {_coroutine} has a null value");
    }

    private IEnumerator Count()
    {
        for (int i = 0; i < _time; i++)
        {
            yield return new WaitForSecondsRealtime(_delay);
            int remains = _time - i;

            Counted?.Invoke(remains);
        }

        _coroutine = null;
        AccountOver?.Invoke();
    }
}