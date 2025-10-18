using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;
    int _currentIndexClip = 0;
    [SerializeField] List<AudioClip> clipList;

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
