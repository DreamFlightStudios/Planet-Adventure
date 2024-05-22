using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAiming : MonoBehaviour
{
    private PlayerInput _playerInput;
    private bool _isAiming;
    private int _targetWeight;
    float k;

    [SerializeField] private float _sensivity;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Rig _handRig;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Aim(InputAction.CallbackContext context) => _isAiming = !_isAiming;

    private void Update()
    {
        k = Mathf.Lerp(k, _targetWeight, Time.deltaTime * 10f);
        if (!_isAiming)
        {
            _targetWeight = 1;
        }
        else
        {
            _targetWeight = 0;
        }
        _aimRig.weight = k;
        _handRig.weight = k;
        Debug.Log(k);
    }

    private void RotatePlayer()
    {
        Vector2 input = _playerInput.Player.LookInput.ReadValue<Vector2>() * _sensivity * Time.deltaTime;

        _animator.SetFloat("TurnVelocity", input.x);
    }

    private void OnEnable()
    {
        _playerInput.Player.AimInput.started += Aim;
    }

    private void OnDisable()
    {
        _playerInput.Player.AimInput.started -= Aim;
    }
}
