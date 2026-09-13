using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour
{
    protected IPausable PausableObject {  get; private set; }
    private IPauseService _pauseService;

    private void Awake()
        => PausableObject = GetComponent<IPausable>();

    [Inject]
    private void Construct(IPauseService pauseService)
    {
        _pauseService = pauseService;
        _pauseService.PauseChanged += OnPaused;
    }

    private void OnPaused(bool state)
    {
        if (state)
            OnGamePaused();
        else
            OnGameResumed();
    }

    protected virtual void OnGamePaused()
    {
        if (PausableObject != null)
        {
            PausableObject.OnPause();
        }
    }

    protected virtual void OnGameResumed()
    {
        if (PausableObject != null)
        {
            PausableObject.OnResume();
        }
    }

    private void OnDestroy()
    {
        if (_pauseService != null)
            _pauseService.PauseChanged -= OnPaused;
    }
}
