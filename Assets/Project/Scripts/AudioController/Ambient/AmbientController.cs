using System.Collections;
using UnityEngine;
using Zenject;

public class AmbientController : MonoBehaviour
{
    protected AmbientItem CurrentAmbient { get; private set; }

    [SerializeField] private bool _playOnStart;
    [SerializeField] private AmbientInfo _info;
    private AudioController _controller;

    [Inject]
    private void Construct(AudioController controller)
        => _controller = controller;

    protected virtual void Start()
    {
        if (_playOnStart)
            StartPlaying();
    }

    public void StartPlaying() 
        => StartCoroutine(PlayLoop());

    protected IEnumerator PlayLoop()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_info.GetStartRandomDelay());

            CurrentAmbient = _info.GetRandomAmbientItem();
            _controller.Play(CurrentAmbient.Clip, SourceType.Ambient, CurrentAmbient.FadeInDelay);

            yield return new WaitForSecondsRealtime(_info.GetRandomDelayFromAmbientItem(CurrentAmbient));
        }
    }

    public void StopPlaying()
    {
        StopCoroutine(PlayLoop());
        _controller.Stop(SourceType.Ambient, CurrentAmbient.FadeOutDelay);
    }

    private void OnDestroy() 
        => StopPlaying();
}