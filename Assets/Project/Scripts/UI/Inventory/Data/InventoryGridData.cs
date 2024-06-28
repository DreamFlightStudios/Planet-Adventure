using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryGridData : MonoBehaviour
{
    public List<InventorySlotData> Slots;
    public Vector2Int Size;
}
