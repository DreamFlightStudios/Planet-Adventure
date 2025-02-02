using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header("Dependencies")]
    [SerializeField] private RootViewUI _rootUI;
    [SerializeField] private Coroutines _coroutines;
    [SerializeField] private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        var rootViewUI = Container.InstantiatePrefabForComponent<RootViewUI>(_rootUI);
        Container.Bind<RootViewUI>().FromInstance(rootViewUI).AsSingle();

        var sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader);
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();

        var coroutines = Container.InstantiatePrefabForComponent<Coroutines>(_coroutines);
        Container.Bind<Coroutines>().FromInstance(coroutines).AsSingle();

        var input = new InputSystem();
        Container.Bind<InputSystem>().FromInstance(input).AsSingle();
        input.Enable();
    }
}