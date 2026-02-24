using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header("Dependencies")]
    [SerializeField] private RootControllerUI _rootUI;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Coroutines _coroutines;
    [SerializeField] private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        var input = new InputSystem();
        Container.Bind<InputSystem>().FromInstance(input).AsSingle();
        input.Enable();

        var saveLoadController = new SaveLoadController();
        Container.Bind<SaveLoadController>().FromInstance(saveLoadController).AsSingle();

        var rootControllerUI = Container.InstantiatePrefabForComponent<RootControllerUI>(_rootUI);
        Container.Bind<RootControllerUI>().FromInstance(rootControllerUI).AsSingle();
        rootControllerUI.Initialize(input);

        var sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader);
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();

        var coroutines = Container.InstantiatePrefabForComponent<Coroutines>(_coroutines);
        Container.Bind<Coroutines>().FromInstance(coroutines).AsSingle();

        var audioController = Container.InstantiatePrefabForComponent<AudioController>(_audioController);
        Container.Bind<AudioController>().FromInstance(audioController).AsSingle();
    }
}