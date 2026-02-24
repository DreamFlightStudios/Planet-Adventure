using System.Collections;
using UnityEngine;
using Zenject;

public class AmbientController : MonoBehaviour
{
    [SerializeField] private AmbientInfo _info;

    private AudioController _controller;
    private AmbientItem _currentAmbient;

    [Inject]
    private void Construct(AudioController controller)
        => _controller = controller;

    private IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_info.GetStartRandomDelay());

            _currentAmbient = _info.GetRandomAmbientItem();
            _controller.Play(_currentAmbient.Clip, SourceType.Ambient, _currentAmbient.FadeInDelay);

            yield return new WaitForSecondsRealtime(_info.GetRandomDelayFromAmbientItem(_currentAmbient));
        }
    }
}