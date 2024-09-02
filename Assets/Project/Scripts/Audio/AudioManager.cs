using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _defaultSource;
    [SerializeField] private AudioSource _uiSoursce;
    [SerializeField] private AudioSource _dispatherSource;
    [SerializeField] private AudioSource _interactionSource;

    public void PlaySound(AudioClip clip, SoundType type)
    {
        AudioSource calledSource;
        switch (type)
        {
            case SoundType.UI :
                calledSource = _uiSoursce;
                break;

            case SoundType.Interaction :
                calledSource = _interactionSource;
                break;

            case SoundType.Dispatcher :
                calledSource = _dispatherSource;
                break;

            default : 
                calledSource = _defaultSource;
                break;
        }
        calledSource.PlayOneShot(clip);
    }
}

public enum SoundType
{
    UI,
    Interaction,
    Dispatcher
}
