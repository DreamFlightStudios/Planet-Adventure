using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenuControllerUI : UIScreen
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _pauseMenu;

    [Header("PopUps")]
    [SerializeField] private PopUpConfig _backMenuPopUp;

    private SceneLoader _sceneLoader;
    private LevelsConfig _levelsConfig;
    private IPauseService _pauseService;
    private IPopUpService _popUpService;

    [Inject]
    private void Construct(LevelsConfig levelsConfig, IPauseService pauseService, IPopUpService popUpService)
    {
        _levelsConfig = levelsConfig;
        _pauseService = pauseService;
        _popUpService = popUpService;
    }

    private void Awake()
        => _pauseMenu.SetActive(false);

    public void Initialize(SceneLoader sceneLoader, RootControllerUI uiScreens)
    {
        _sceneLoader = sceneLoader;

        _continueButton.onClick.AddListener(() => SwitchState(false));
        _backMenuButton.onClick.AddListener(OnBackMenuButonClicked);
        _settingsButton.onClick.AddListener(() => uiScreens.Show(ScreenId.Settings));
    }

    public override void SwitchState(bool state)
        => SwitchStateByContainer(state, _pauseMenu);

    protected override void OnShow()
        => _pauseService.Pause(this);

    protected override void OnHide()
        => _pauseService.Resume(this);

    protected override void OnDispose()
        => _pauseService.Resume(this);

    private void OnBackMenuButonClicked()
        => _popUpService.Show(_backMenuPopUp, OnBackMenuPopUpClosed);

    private void OnBackMenuPopUpClosed(PopUpResult result)
    {
        if (result != PopUpResult.Accepted)
            return;

        _continueButton.onClick.RemoveAllListeners();
        _backMenuButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();

        _sceneLoader.ChangeScene(_levelsConfig.MainMenuSceneName);
    }
}
