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
    //[SerializeField] TextMeshProUGUI textVolume;


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20f);
        //int normValue = (int)((volume + 80) / (sliderVolume.maxValue + 80) * 100);
        //textVolume.text = Convert.ToString(normValue);
    }
}
