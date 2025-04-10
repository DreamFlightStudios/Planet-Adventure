using UnityEngine.Events;

public interface IInteractive
{
    UnityEvent Interacted { get; }
    string Context {  get; }
    bool CanInteract { get; }
    void Interaction();
}