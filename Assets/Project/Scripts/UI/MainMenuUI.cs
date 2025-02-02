using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public void Initialize(SceneLoader sceneLoader) 
        => _playButton.onClick.AddListener(sceneLoader.LoadGameplayScene);
}