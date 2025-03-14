using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGamePopup : MonoBehaviour
{
    [SerializeField] private string _defaultSaveName = "New Game";
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private TMP_Text _inputField;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    public void Initialize(SceneLoader sceneLoader) 
        => _sceneLoader = sceneLoader;

    private void OnConfirmButtonClicked() 
        => _sceneLoader.LoadGameplayScene(_inputField.text);

    private void OnCancelButtonClicked()
    {
        _inputField.text = _defaultSaveName;
        gameObject.SetActive(false);
    }
}