using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public Slider volSlider;

    void Start()
    {
        Load();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            SetVolume(PlayerPrefs.GetFloat("masterVolume"));
        }
        else
        {
            SetVolume(1f);
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volSlider.value);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        GameManager.audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        volSlider.value = volume;
        Save();
    }
}