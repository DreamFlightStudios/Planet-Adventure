using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderCounter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _counter;

    private void Start()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(_slider.value);
    }

    private void OnValueChanged(float value) 
        => _counter.text = value.ToString();
}