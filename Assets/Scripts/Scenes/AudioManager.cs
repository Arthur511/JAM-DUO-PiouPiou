using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;

    [Header("Weapon AudioSource")]
    public AudioSource TriangleAudioSource;
    public AudioSource FluteAudioSource;
    public AudioSource TimpaniAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void PlayASound(AudioSource source)
    {
        source.Play();
    }

}
