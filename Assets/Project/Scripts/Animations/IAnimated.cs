using System;

public interface IAnimated
{
    public event Action<float, float> Moved;
}
