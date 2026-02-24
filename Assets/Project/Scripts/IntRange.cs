using System;

[Serializable]
public struct IntRange
{
    public int Min;
    public int Max;

    public IntRange(int min, int max)
    {
        Min = min;
        Max = max;
    }
}
