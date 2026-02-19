using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour
{
    protected IPausable PausableObject {  get; private set; }
    private PauseMenuControllerUI _pauseMenu;

    private void Awake() 
        => PausableObject = GetComponent<IPausable>();

    [Inject]
    private void Construct(PauseMenuControllerUI pauseMenu)
    {
        _pauseMenu = pauseMenu;
        _pauseMenu.Paused += OnPaused;
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
        if (_pauseMenu != null)
            _pauseMenu.Paused -= OnPaused;
    }
}