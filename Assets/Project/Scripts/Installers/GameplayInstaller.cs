using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private WarningIndicator _warningIndicatorPrefab;
    [SerializeField] private PauseMenuControllerUI _pauseMenuPrefab;
    [SerializeField] private SubtilesUI _subtitlesPrefab;

    public override void InstallBindings()
    {
        var pauseMenu = Container.InstantiatePrefabForComponent<PauseMenuControllerUI>(_pauseMenuPrefab);
        Container.Bind<PauseMenuControllerUI>().FromInstance(pauseMenu).AsSingle();

        var warningIndicator = Container.InstantiatePrefabForComponent<WarningIndicator>(_warningIndicatorPrefab);
        Container.Bind<WarningIndicator>().FromInstance(warningIndicator).AsSingle();

        var subtitles = Container.InstantiatePrefabForComponent<SubtilesUI>(_subtitlesPrefab);
        Container.Bind<SubtilesUI>().FromInstance(subtitles).AsSingle();
    }
}