using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resolution : MonoBehaviour
{
    public List<UnityEngine.Resolution> filteredResolutions;
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        FilterResolutions();
        SetDropdownOptions();
        SetCurrentResolution();
    }

    private void FilterResolutions()
    {
        // Get all the  available resolutions for the user's screen
        UnityEngine.Resolution[] allResolutions = Screen.resolutions;

        filteredResolutions = new List<UnityEngine.Resolution>();

        // Filter to use only 16:9 aspect ratio
        foreach (UnityEngine.Resolution res in allResolutions)
        {
            int gcd = GCD(res.width, res.height);

            int widthRatio = res.width / gcd;
            int heightRatio = res.height / gcd;

            if (widthRatio == 16 && heightRatio == 9)
            {
                // Avoid adding duplicate resolutions
                bool isDuplicate = filteredResolutions.Exists(x =>
                    x.width == res.width && x.height == res.height && x.refreshRate == res.refreshRate);

                if (!isDuplicate)
                {
                    filteredResolutions.Add(res); // Add it to the list of filtered resolutions
                }
            }
        }
    }

    private void SetDropdownOptions()
    {
        List<string> options = new List<string>();

        foreach (UnityEngine.Resolution res in filteredResolutions)
        {
            // Create a string for each resolution to add to the dropdown
            string option = res.width + " x " + res.height + " @" + res.refreshRate + "Hz";
            options.Add(option);
        }

        // Add all options to the dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

    }

    private void SetCurrentResolution()
    {
        UnityEngine.Resolution currentResolution = Screen.currentResolution;
        int index = filteredResolutions.FindIndex(r => r.width == currentResolution.width && r.height == currentResolution.height && r.refreshRate == currentResolution.refreshRate);
        if (index != -1)
        {
            resolutionDropdown.value = index;
            resolutionDropdown.RefreshShownValue();
        }
    }

    // Helper method to calculate the Greatest Common Divisor (GCD) using Euclid's algorithm
    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        PlayerPrefs.SetString("CurrentResolution", resolution.ToString());
    }

    public void ResetToInitialSettings()
    {
        string initialRes = PlayerPrefs.GetString("InitialResolution", $"{Screen.currentResolution.width} x {Screen.currentResolution.height} @ {Screen.currentResolution.refreshRate}Hz");
        var resParts = initialRes.Split(new char[] { 'x', '@' });

        if (resParts.Length >= 3)
        {
            bool parseWidth = int.TryParse(resParts[0].Trim(), out int width);
            bool parseHeight = int.TryParse(resParts[1].Trim(), out int height);
            string refreshPart = resParts[2].Trim();
            int refreshRatePos = refreshPart.IndexOf("Hz");
            bool parseRefreshRate = int.TryParse(refreshPart.Substring(0, refreshRatePos).Trim(), out int refreshRate);

            if (parseWidth && parseHeight && parseRefreshRate)
            {
                int index = filteredResolutions.FindIndex(r => r.width == width && r.height == height && r.refreshRate == refreshRate);
                if (index != -1)
                {
                    resolutionDropdown.value = index;
                    resolutionDropdown.RefreshShownValue();
                    Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
                }
                else
                {
                    Debug.LogError("Failed to find matching resolution in filtered resolutions");
                }
            }
            else
            {
                Debug.LogError("Failed to parse resolution settings from PlayerPrefs");
            }
        }
        else
        {
            Debug.LogError("Initial resolution string format is incorrect: " + initialRes);
        }
    }
}
