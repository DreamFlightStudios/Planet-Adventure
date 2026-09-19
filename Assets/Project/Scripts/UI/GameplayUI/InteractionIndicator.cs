using DG.Tweening;
using TMPro;
using UnityEngine;

public class InteractionIndicator : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _indicator;
    [SerializeField] private TMP_Text _contextField;
    [SerializeField] private Hand _interactor;
    [SerializeField] private Camera _camera;

    [Header("Follow")]
    [SerializeField] private Vector3 _worldOffset = new Vector3(0.0f, 0.25f, 0.0f);
    [SerializeField] private float _followSmoothTime = 0.08f;
    [SerializeField] private float _screenMargin = 80.0f;

    [Header("Floating")]
    [SerializeField] private Vector2 _floatingAmplitude = new Vector2(8.0f, 12.0f);
    [SerializeField] private float _floatingSpeed = 1.4f;

    [Header("Appearance")]
    [SerializeField] private float _appearDuration = 0.15f;

    private Canvas _canvas;
    private RectTransform _canvasRect;
    private RectTransform _indicatorRect;

    private Transform _target;
    private Vector2 _followVelocity;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvasRect = (RectTransform)transform;
        _indicatorRect = _indicator.GetComponent<RectTransform>();
        _indicatorRect.anchorMin = _indicatorRect.anchorMax = new Vector2(0.5f, 0.5f);

        _interactor.TargetChanged += OnTargetChanged;

        _indicator.SetActive(false);
    }

    private void LateUpdate()
    {
        if (_target == null || ResolveCamera() == null)
            return;

        _indicatorRect.localPosition = Vector2.SmoothDamp(_indicatorRect.localPosition, GetTargetPoint(), ref _followVelocity, _followSmoothTime);
    }

    public void OnTargetChanged(IInteractive target)
    {
        if (target == null)
        {
            _target = null;
            Hide();
            return;
        }

        _target = target.Anchor;
        _contextField.text = target.Context;
        Show();
    }

    private void Show()
    {
        if (_indicator.activeSelf)
            return;

        _indicator.SetActive(true);
        _followVelocity = Vector2.zero;

        if (ResolveCamera() != null)
            _indicatorRect.localPosition = GetTargetPoint();

        _indicatorRect.DOKill();
        _indicatorRect.localScale = Vector3.zero;
        _indicatorRect.DOScale(Vector3.one, _appearDuration);
    }

    private void Hide()
    {
        _indicatorRect.DOKill();
        _indicatorRect.localScale = Vector3.one;
        _indicator.SetActive(false);
    }

    private Vector2 GetTargetPoint()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(_target.position + _worldOffset);

        if (screenPoint.z < 0.0f)
        {
            screenPoint.x = Screen.width - screenPoint.x;
            screenPoint.y = Screen.height - screenPoint.y;
        }

        screenPoint.x = Mathf.Clamp(screenPoint.x, _screenMargin, Screen.width - _screenMargin);
        screenPoint.y = Mathf.Clamp(screenPoint.y, _screenMargin, Screen.height - _screenMargin);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPoint, GetCanvasCamera(), out Vector2 localPoint);

        return localPoint + GetFloatingOffset();
    }

    private Vector2 GetFloatingOffset()
        => new Vector2(Mathf.Sin(Time.time * _floatingSpeed) * _floatingAmplitude.x, Mathf.Sin(Time.time * _floatingSpeed * 1.3f) * _floatingAmplitude.y);

    private Camera GetCanvasCamera()
        => _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

    private Camera ResolveCamera()
    {
        if (_camera == null)
            _camera = Camera.main == null ? FindFirstObjectByType<Camera>() : Camera.main;

        return _camera;
    }

    private void OnDestroy()
    {
        _interactor.TargetChanged -= OnTargetChanged;
        _indicatorRect.DOKill();
    }
}
