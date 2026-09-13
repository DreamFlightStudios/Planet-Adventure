using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PopUpCaller : MonoBehaviour
{
    [SerializeField] private PopUpConfig _config;

    [Header("Results")]
    [SerializeField] private UnityEvent _accepted;
    [SerializeField] private UnityEvent _discarded;
    [SerializeField] private UnityEvent _closed;

    private IPopUpService _popUpService;

    [Inject]
    private void Construct(IPopUpService popUpService)
        => _popUpService = popUpService;

    public void Show()
        => _popUpService.Show(_config, OnClosed);

    private void OnClosed(PopUpResult result)
    {
        if (this == null)
            return;

        switch (result)
        {
            case PopUpResult.Accepted:
                _accepted?.Invoke();
                break;
            case PopUpResult.Discarded:
                _discarded?.Invoke();
                break;
            case PopUpResult.Closed:
                _closed?.Invoke();
                break;
        }
    }
}
