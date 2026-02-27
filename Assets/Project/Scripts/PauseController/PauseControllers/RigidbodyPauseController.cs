using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPauseController : PauseController
{
    private Vector3 _savedVelocity;
    private Vector3 _savedAngularVelocity;

    private bool _savedKinematic;
    private bool _savedGravity;

    private Rigidbody _rigidbody;

    private void Awake() 
        => _rigidbody = GetComponent<Rigidbody>();

    protected override void OnGamePaused()
    {
        base.OnGamePaused();

        _savedVelocity = _rigidbody.linearVelocity;
        _savedAngularVelocity = _rigidbody.angularVelocity;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _savedKinematic = _rigidbody.isKinematic;
        _savedGravity = _rigidbody.useGravity;

        _rigidbody.useGravity = false;
    }

    protected override void OnGameResumed()
    {
        base.OnGameResumed();

        _rigidbody.isKinematic = _savedKinematic;
        _rigidbody.useGravity = _savedGravity;

        _rigidbody.linearVelocity = _savedVelocity;
        _rigidbody.angularVelocity = _savedAngularVelocity;
    }
}