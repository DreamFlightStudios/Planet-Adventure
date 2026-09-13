using System;
using UnityEngine;

[Serializable]
public class PopUpButtonInfo
{
    [field: SerializeField] public bool IsActive { get; private set; }
    [field: SerializeField] public string Label { get; private set; }
}
