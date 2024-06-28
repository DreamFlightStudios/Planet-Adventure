using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    [field: SerializeField] public string _name { get; private set; }
    [field: SerializeField] public string _id { get; private set; }
    [field: SerializeField] public string _description { get; private set; }
    [field: SerializeField] public Sprite _icon { get; private set; }
}
