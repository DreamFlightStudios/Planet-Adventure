using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private WarningIndicator _warningIndicatorPrefab;
    [SerializeField] private PauseMenuUI _pauseMenuPrefab;

    public override void InstallBindings()
    {
        var warningIndicator = Container.InstantiatePrefabForComponent<WarningIndicator>(_warningIndicatorPrefab);
        Container.Bind<WarningIndicator>().FromInstance(warningIndicator).AsSingle();

        var pauseMenu = Container.InstantiatePrefabForComponent<PauseMenuUI>(_pauseMenuPrefab);
        Container.Bind<PauseMenuUI>().FromInstance(pauseMenu).AsSingle();
    }
}