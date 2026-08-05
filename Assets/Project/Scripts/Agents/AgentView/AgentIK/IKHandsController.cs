using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKHandsController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _shoulder;
    [SerializeField] private Transform _hand;
    [SerializeField] private TwoBoneIKConstraint _ik;

    [Header("Settings")]
    [SerializeField] private float _weightSpeed;
    [SerializeField] private float _followSpeed;

    private TouchableObject _target;
    private float _weight;

    private void Update()
    {
        if (_target == null)
        {
            _weight = Mathf.Lerp(_weight, 0f, Time.deltaTime * _weightSpeed);
            _ik.weight = _weight;

            return;
        }

        _weight = Mathf.Lerp(_weight, 1f, Time.deltaTime * _weightSpeed);
        _ik.weight = _weight;

        Vector3 targetLocalPos = _hand.parent.InverseTransformPoint(_target.GetClosestPoint(_shoulder.position));
        Vector3 localPos = _hand.localPosition;

        localPos.x = targetLocalPos.x;
        localPos.z = Mathf.Lerp(localPos.z, targetLocalPos.z, Time.deltaTime * _weightSpeed);
        localPos.y = Mathf.Lerp(localPos.y, targetLocalPos.y, Time.deltaTime * _weightSpeed);

        _hand.localPosition = localPos;
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