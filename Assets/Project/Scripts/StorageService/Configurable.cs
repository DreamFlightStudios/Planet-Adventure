using UnityEngine;
using Zenject;

public abstract class Configurable : MonoBehaviour
{
    private SaveLoadController _saveLoadController;

    [Inject]
    private void Construct(SaveLoadController saveLoadController)
    {
        _saveLoadController = saveLoadController;
        _saveLoadController.Loaded += OnConfigurated;
    }

    protected virtual void OnConfigurated(UserData data) { }

    private void OnDestroy()
    {
        if (_saveLoadController != null)
            _saveLoadController.Loaded -= OnConfigurated;
    }
}