using UnityEngine;
using Zenject;

public class GameEnteryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _rootObjects;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        var rootObjects = Instantiate(_rootObjects);
        DontDestroyOnLoad(rootObjects);

        sceneLoader.LoadMainMenu();
    }
}