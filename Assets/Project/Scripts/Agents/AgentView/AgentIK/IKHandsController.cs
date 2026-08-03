using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKHandsController : MonoBehaviour
{
    [SerializeField] private Transform _shoulder;
    [SerializeField] private Transform _hand;
    [SerializeField] private TwoBoneIKConstraint _ik;
    [SerializeField] private float _speed;

    private TouchableObject _target;
    private float _weight;

    private void Update()
    {
        if (_target == null)
        {
            _weight = Mathf.Lerp(_weight, 0f, Time.deltaTime * _speed);
            _ik.weight = _weight;

            return;
        }

        _weight = Mathf.Lerp(_weight, 1f, Time.deltaTime * _speed);
        _ik.weight = _weight;

        _hand.position = _target.GetClosestPoint(_shoulder.position);
        _hand.rotation = _target.HandRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target != null) 
            return;

        var touchable = other.GetComponent<TouchableObject>();

        if (touchable == null || touchable.IsTouched) 
            return;

        _target = touchable;
        touchable.IsTouched = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var touchable = other.GetComponent<TouchableObject>();

        if (touchable == null || _target != touchable) 
            return;

        touchable.IsTouched = false;
        _target = null;
    }
}