using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [Header("Volume Settings")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderVolume;

    public void SetVolume(float volume)
    {
        if (volume > 0)
            audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20f);
        else
            audioMixer.SetFloat("MainVolume", -80f);
    }


}
