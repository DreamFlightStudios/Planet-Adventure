using System;
using UnityEngine;

[Serializable]
public struct Phrase
{
    [field: SerializeField] public PhraseInfo Info;
    [field: SerializeField] public float Delay;
}