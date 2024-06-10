using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAiming : MonoBehaviour
{
    private int _targetWeight;
    float k;

    [SerializeField] private float _sensivity;
    [SerializeField] private ThirdPersonController _thirdPersonController;

    [Header("Rig")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Rig _handRig;

    private PlayerInput _playerInput;
    private bool _isAiming;

    public event Action<bool> Aimed;

    [Inject]
    private void Construct(PlayerInput input) => _playerInput = input;

    private void Aim(InputAction.CallbackContext context)
    {
        _isAiming = !_isAiming;
        Aimed.Invoke(_isAiming);
    }

    private void FixedUpdate()
    {
        k = Mathf.Lerp(k, _targetWeight, Time.deltaTime * 10f);
        if (_isAiming)
        {
            _targetWeight = 1;
        }
        else
        {
            _targetWeight = 0;
        }

        _handRig.weight = k;
        if(_handRig.weight < 0.1 || _handRig.weight == 0.9)
        {
            _aimRig.weight = k;
        }
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
