using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSource")]
    [SerializeField] private AudioSource _sfxSource;

    [Header("AudioClip")]
    public AudioClip flipSound;
    public AudioClip misMatchSound;
    public AudioClip matchSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        _sfxSource.PlayOneShot(clip);
    }

    public void PlayFlip() => PlaySFX(flipSound);
    public void PlayMisMatch() => PlaySFX(misMatchSound);
    public void PlayMatch() => PlaySFX(matchSound);
    public void PlayWin() => PlaySFX(winSound);
    public void PlayLose() => PlaySFX(loseSound);
}
