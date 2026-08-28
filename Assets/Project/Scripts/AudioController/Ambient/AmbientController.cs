using System.Collections;
using UnityEngine;
using Zenject;

public class AmbientController : MonoBehaviour
{
    protected AmbientItem CurrentAmbient { get; private set; }

    [SerializeField] private bool _playOnStart;
    [SerializeField] private AmbientInfo _info;

    private AudioController _controller;
    private Coroutine _ambientCoroutine;
    private bool _isPlaying;

    [Inject]
    private void Construct(AudioController controller)
        => _controller = controller;

    protected virtual void Start()
    {
        if (_playOnStart)
            StartPlaying();
    }

    public void StartPlaying()
    {
        if (_isPlaying || _ambientCoroutine != null)
            return;

        _isPlaying = true;
        _ambientCoroutine = StartCoroutine(PlayLoop());
    }

    protected IEnumerator PlayLoop()
    {
        while (_isPlaying)
        {
            yield return new WaitForSecondsRealtime(_info.GetStartRandomDelay());

            Debug.Log(_isPlaying);
            if (!_isPlaying) 
                yield break;

            CurrentAmbient = _info.GetRandomAmbientItem();
            if (CurrentAmbient.Clip == null)
            {
                Debug.LogWarning("Ambient item has null clip, skipping...");
                continue;
            }

            _controller.Play(CurrentAmbient.Clip, SourceType.Ambient, CurrentAmbient.FadeInDelay);

            yield return new WaitForSecondsRealtime(_info.GetRandomDelayFromAmbientItem(CurrentAmbient));
        }
    }

    public void StopPlaying()
    {
        _isPlaying = false;

        if (_ambientCoroutine != null)
        {
            StopCoroutine(_ambientCoroutine);
            _ambientCoroutine = null;
        }

        if (_controller != null && CurrentAmbient.Clip != null)
            _controller.Stop(SourceType.Ambient, CurrentAmbient.FadeOutDelay);
    }

    private void OnDestroy() 
        => StopPlaying();
}