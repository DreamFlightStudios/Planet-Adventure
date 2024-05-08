using System;

public interface IAnimationObject
{
    public event Action<float, float> Moved;
}
