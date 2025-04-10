using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private WarningIndicator _warningIndicatorPrefab;
    [SerializeField] private PauseMenuUI _pauseMenuPrefab;
    [SerializeField] private SubtilesUI _subtitlesPrefab;

    public override void InstallBindings()
    {
        var warningIndicator = Container.InstantiatePrefabForComponent<WarningIndicator>(_warningIndicatorPrefab);
        Container.Bind<WarningIndicator>().FromInstance(warningIndicator).AsSingle();

        var subtitles = Container.InstantiatePrefabForComponent<SubtilesUI>(_subtitlesPrefab);
        Container.Bind<SubtilesUI>().FromInstance(subtitles).AsSingle();

        var pauseMenu = Container.InstantiatePrefabForComponent<PauseMenuUI>(_pauseMenuPrefab);
        Container.Bind<PauseMenuUI>().FromInstance(pauseMenu).AsSingle();
    }
}