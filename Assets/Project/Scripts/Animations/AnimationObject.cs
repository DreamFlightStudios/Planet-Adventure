using System;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    public virtual event Action<float, float> Moved;
}
