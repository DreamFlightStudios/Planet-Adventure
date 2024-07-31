using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSourse;
    [SerializeField] private AudioSource _dispatherSourse;
    [SerializeField] private AudioSource _interactionSourse;

    public void PlaySound(AudioClip clip, SoundType type)
    {
        switch (type)
        {
            case SoundType.UI:
                _uiSourse.PlayOneShot(clip);
                break;

            case SoundType.Interaction :
                _interactionSourse.PlayOneShot(clip);
                break;

            case SoundType.Dispatcher:
                _dispatherSourse.PlayOneShot(clip);
                break;
        }
    }
}

public enum SoundType
{
    UI,
    Interaction,
    Dispatcher
}
