using UnityEngine;
using Zenject;

public abstract class Configurable : MonoBehaviour
{
    private ISettingsService _settingsService;

    [Inject]
    private void Construct(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        _settingsService.Subscribe(OnConfigurated);
    }

    // Subscribe applies the current settings immediately, and Zenject injects before Awake,
    // so OnConfigurated may run earlier than Awake. Rely only on [SerializeField] fields here.
    protected virtual void OnConfigurated(UserSettingsData settings) { }

    private void OnDestroy()
    {
        if (_settingsService != null)
            _settingsService.Unsubscribe(OnConfigurated);
    }
}
