using System;

public interface IAnimated
{
    public event Action<float> Moved;
    public event Action<float> Rotated;
}
