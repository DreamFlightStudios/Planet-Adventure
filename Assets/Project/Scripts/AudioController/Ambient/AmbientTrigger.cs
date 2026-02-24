using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class AmbientTrigger : MonoBehaviour
{
    [SerializeField] private AmbientInfo _info;

    private AudioController _controller;
    private bool _isInside;

    [Inject]
    private void Construct(AudioController controller) 
        => _controller = controller;

    private IEnumerator PlayAmbient()
    {
        while (_isInside)
        {
            yield return new WaitForSecondsRealtime(_info.GetStartRandomDelay());

            _controller.Play(_info.GetRandomClip(), SourceType.Ambient);
            yield return new WaitForSecondsRealtime(_info.GetRandomDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isInside = true;
        StartCoroutine(PlayAmbient());
    }

    private void OnTriggerExit(Collider other)
    {
        _isInside = false;

        _controller.Stop(SourceType.Ambient);
        StopCoroutine(PlayAmbient());
    }

    private void OnDrawGizmosSelected()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(collider.center, collider.size);
    }
}