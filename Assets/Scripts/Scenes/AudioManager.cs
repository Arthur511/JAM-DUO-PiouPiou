using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public bool IsInMenu = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
