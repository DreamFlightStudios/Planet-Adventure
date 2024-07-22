using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    private LineRenderer _renderer;

    private void Awake() => _renderer = GetComponent<LineRenderer>();

    void FixedUpdate()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _renderer.SetPosition(i, _points[i].position);
        }
    }
}
