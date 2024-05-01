using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public Toggle fullscreenToggle;

    void Start()
    {
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Get the fullscreen setting from PlayerPrefs and update the toggle
        bool isFullscreen = PlayerPrefs.GetInt("FullscreenSetting", 1) == 1; // Default to true if not found
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("FullscreenSetting", isFullscreen ? 1 : 0);
    }
}