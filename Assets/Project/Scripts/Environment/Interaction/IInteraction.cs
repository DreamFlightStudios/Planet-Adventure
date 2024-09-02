using UnityEngine;

public interface IInteractive
{
    public bool CanInteract { get; }
    public AudioClip InteractionSound {  get; }
    public void Interaction();
}