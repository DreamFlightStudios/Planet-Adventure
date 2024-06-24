using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;

    protected void EnableDisable(bool state) => _indicator.SetActive(state);
}
