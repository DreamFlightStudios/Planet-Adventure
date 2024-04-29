using System;
using UnityEngine;

public abstract class AnimationObject : MonoBehaviour
{
    public abstract event Action<float, float> Moved;
}
