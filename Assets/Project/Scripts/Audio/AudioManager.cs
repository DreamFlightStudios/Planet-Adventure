using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _defoultSourse;
    [SerializeField] private AudioSource _uiSourse;
    [SerializeField] private AudioSource _dispatherSourse;
    [SerializeField] private AudioSource _interactionSourse;

    public void PlaySound(AudioClip clip, SoundType type)
    {
        AudioSource calledSourse;
        switch (type)
        {
            case SoundType.UI :
                calledSourse = _uiSourse;
                break;

            case SoundType.Interaction :
                calledSourse = _interactionSourse;
                break;

            case SoundType.Dispatcher :
                calledSourse = _dispatherSourse;
                break;

            default : 
                calledSourse = _defoultSourse;
                break;
        }
        calledSourse.PlayOneShot(clip);
    }
}

public enum SoundType
{
    UI,
    Interaction,
    Dispatcher
}
