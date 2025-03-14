using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _choiceGameSaveButton;
    [SerializeField] private Button _createNewGameButton;

    [SerializeField] private GameObject _savedGamesPanel;
    [SerializeField] private CreateGamePopup _createGamePopup;

    private SceneLoader _sceneLoader;

    private void Start()
    {
        _savedGamesPanel.SetActive(false);
        _createGamePopup.gameObject.SetActive(false);
    }

    public void Initialize(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        _createGamePopup.Initialize(sceneLoader);

        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _createNewGameButton.onClick.AddListener(OnCreateNewGameButtonClicked);
        _choiceGameSaveButton.onClick.AddListener(OnChoiceGameSaveButtonClicked);
    }

    public void OnPlayButtonClicked() 
        => _sceneLoader.LoadGameplayScene();

    public void OnCreateNewGameButtonClicked() 
        => _createGamePopup.gameObject.SetActive(true);

    public void OnChoiceGameSaveButtonClicked() 
        => _savedGamesPanel.SetActive(!_savedGamesPanel.activeSelf);
}