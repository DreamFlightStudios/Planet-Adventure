using UnityEngine;

public class StorableContainer : MonoBehaviour
{
    [field: SerializeField] public InteractiveObject[] InteractiveObjects { get; private set; }
}