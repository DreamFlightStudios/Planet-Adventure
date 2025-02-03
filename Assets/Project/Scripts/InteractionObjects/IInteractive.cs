using UnityEngine;
using UnityEngine.Events;

public interface IInteractive
{
    AudioClip InteractionSound { get; }
    UnityEvent Interacted { get; }
    string Context {  get; }
    bool CanInteract { get; }
    void Interaction();
}