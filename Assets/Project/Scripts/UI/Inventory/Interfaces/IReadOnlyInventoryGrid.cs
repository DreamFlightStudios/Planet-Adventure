using System;
using UnityEngine;

public interface IReadOnlyInventoryGrid : IReadOnlyInventory
{
    event Action<Vector2Int> SizeChanged;
    Vector2Int Size { set; }
    IReadOnlyInventorySlot[,] GetSlots();
}
