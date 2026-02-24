using System;

[Serializable]
public struct IntRange
{
    public int Min { get; private set; }
    public int Max { get; private set; }

    public IntRange(int min, int max)
    {
        Min = min;
        Max = max;
    }
}
