using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LaunchLevelsPanel : MonoBehaviour
{
    public bool IsOpen {  get; private set; }

    [SerializeField] private LaunchLevelButton _buttonPrefab;
    [SerializeField] private Transform _buttonsContainer;
    [SerializeField] private float _switchDuration;

    private readonly List<LaunchLevelButton> _buttons = new();

    private SceneLoader _sceneLoader;
    private ILevelProgressService _levelProgress;

    [Inject]
    private void Construct(SceneLoader sceneLoader, ILevelProgressService levelProgress)
    {
        _sceneLoader = sceneLoader;
        _levelProgress = levelProgress;
        _levelProgress.ProgressChanged += OnProgressChanged;
    }

    private void Awake()
        => transform.DOScaleX(0.0f, 0.0f);

    private void Start()
        => RebuildButtons();

    public void SwitchPanel()
    {
        int scale = IsOpen ? 0 : 1;
        IsOpen = !IsOpen;

        transform.DOScaleX(scale, _switchDuration);
    }

    private void RebuildButtons()
    {
        ClearButtons();

        var levels = _levelProgress.Levels;

        for (int i = 0; i < levels.Count; i++)
        {
            var level = levels[i];
            var button = Instantiate(_buttonPrefab, _buttonsContainer);

            button.Initialize(level, _levelProgress.IsUnlocked(level.LevelId));
            button.ButtonPressed += OnLaunchLevelButtonPressed;

            _buttons.Add(button);
        }
    }

    private void ClearButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].ButtonPressed -= OnLaunchLevelButtonPressed;
            Destroy(_buttons[i].gameObject);
        }

        _buttons.Clear();
    }

    private void OnProgressChanged()
        => RebuildButtons();

    private void OnLaunchLevelButtonPressed(string levelId)
        => _sceneLoader.ChangeScene(levelId);

    private void OnDestroy()
    {
        ClearButtons();

        if (_levelProgress != null)
            _levelProgress.ProgressChanged -= OnProgressChanged;
    }
}
