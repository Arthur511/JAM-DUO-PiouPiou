using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the element who manage sounds and musics in the scene
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
    int _currentIndexClip = 0;
    [SerializeField] List<AudioClip> clipList;

    [Header("Weapon AudioSource")]
    public AudioSource TriangleAudioSource;
    //public AudioSource TrumpetAudioSource;
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
        audioSource.PlayOneShot(clipList[_currentIndexClip]);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            _currentIndexClip++;
            if (_currentIndexClip == clipList.Count)
            {
                _currentIndexClip = 0;
            }
            audioSource.PlayOneShot(clipList[_currentIndexClip]);
        }
    }

    public void PlayASound(AudioSource source)
    {
        source.Play();
    }

}
