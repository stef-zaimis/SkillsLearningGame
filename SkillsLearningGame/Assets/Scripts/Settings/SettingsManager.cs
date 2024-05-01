using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Resolution resolution;
    public Fullscreen fullscreen;
    public Accessibility accessibility;
    public SoundSlider volume;
    public Graphics graphics;

    void Start()
    {
        //PlayerPrefs.DeleteAll(); // This was here so that starting with faulty settings wouldnt mess up stuff
        //  Save all the settings of the first launch
        if (!PlayerPrefs.HasKey("IsFirstLaunch"))
        {
            SaveInitialSettings();
            PlayerPrefs.SetInt("IsFirstLaunch", 0); // Mark first launch as completed
            PlayerPrefs.Save();
        }

    }

    void SaveInitialSettings()
    {
        PlayerPrefs.SetInt("InitialFullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("InitialQuality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetString("InitialResolution", $"{Screen.currentResolution.width} x {Screen.currentResolution.height} @ {Screen.currentResolution.refreshRate}Hz");
        PlayerPrefs.SetFloat("InitialVolume", volume.volSlider.value);
        PlayerPrefs.SetInt("InitialColorScheme", accessibility.accessibilityDropdown.value);

        PlayerPrefs.Save();
    }


    public void ResetSettings()
    {
        // Reset to fullscreen
        fullscreen.fullscreenToggle.isOn = true;
        fullscreen.SetFullscreen(true);

        // Reset  resolution settings
        resolution.ResetToInitialSettings();

        // Reset graphics quality to saved initial setting
        int initialQuality = PlayerPrefs.GetInt("InitialQuality");
        graphics.SetQuality(initialQuality);

        // Reset volume to saved initial setting
        volume.SetVolume(1);
        volume.volSlider.value = 1;

        // Reset accessibility settings to saved initial setting
        int initialColorScheme = PlayerPrefs.GetInt("InitialColorScheme");
        accessibility.accessibilityDropdown.value = initialColorScheme;
        accessibility.ApplyColorScheme(initialColorScheme);
    }
}
