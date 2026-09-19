using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    private const string SettingsFileName = "Settings";
    private const string LevelsProgressFileName = "LevelsProgress";
    private const string PolicyAgreementFileName = "PolicyAgreement";

    [Header("Configs")]
    [SerializeField] private SettingsConfigurationConfig _defaultSettings;
    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private PolicyConfig _policyConfig;

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

        Container.Bind<LevelsConfig>().FromInstance(_levelsConfig).AsSingle();

        var storage = new FileStorage();
        Container.Bind<IFileStorage>().FromInstance(storage).AsSingle();

        var serializer = new JsonSaveSerializer();
        Container.Bind<ISaveSerializer>().FromInstance(serializer).AsSingle();

        var settingsFile = new SaveFile<UserSettingsData>(SettingsFileName, UserSettingsData.CurrentVersion, storage, serializer, () => SettingsService.CreateDefault(_defaultSettings));
        var settingsService = new SettingsService(settingsFile, _defaultSettings);
        Container.Bind<ISettingsService>().FromInstance(settingsService).AsSingle();
        settingsService.Initialize();

        var progressFile = new SaveFile<LevelsProgressData>(LevelsProgressFileName, LevelsProgressData.CurrentVersion, storage, serializer, () => new LevelsProgressData());
        var progressService = new LevelProgressService(progressFile, _levelsConfig);
        Container.Bind<ILevelProgressService>().FromInstance(progressService).AsSingle();
        progressService.Initialize();

        var pauseService = new PauseService(input);
        Container.Bind<IPauseService>().FromInstance(pauseService).AsSingle();

        var audioController = Container.InstantiatePrefabForComponent<AudioController>(_audioController);
        Container.Bind<AudioController>().FromInstance(audioController).AsSingle();

        var rootControllerUI = Container.InstantiatePrefabForComponent<RootControllerUI>(_rootUI);
        Container.Bind<RootControllerUI>().FromInstance(rootControllerUI).AsSingle();
        rootControllerUI.Initialize(input);

        var sceneLoader = Container.InstantiatePrefabForComponent<SceneLoader>(_sceneLoader);
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
        sceneLoader.LoadFinished += pauseService.ResetState;

        var coroutines = Container.InstantiatePrefabForComponent<Coroutines>(_coroutines);
        Container.Bind<Coroutines>().FromInstance(coroutines).AsSingle();

        var popUpService = new PopUpService(rootControllerUI.PopUp, rootControllerUI, pauseService, sceneLoader);
        Container.Bind<IPopUpService>().FromInstance(popUpService).AsSingle();
        rootControllerUI.SettingsMenu.Initialize(popUpService);

        var policyAgreementFile = new SaveFile<PolicyAgreementData>(PolicyAgreementFileName, PolicyAgreementData.CurrentVersion, storage, serializer, () => new PolicyAgreementData());
        var policyAgreementService = new PolicyAgreementService(policyAgreementFile, _policyConfig, popUpService);
        Container.Bind<IPolicyAgreementService>().FromInstance(policyAgreementService).AsSingle();
        policyAgreementService.Initialize();
    }
}