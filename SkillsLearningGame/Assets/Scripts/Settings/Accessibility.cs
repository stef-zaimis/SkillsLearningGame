using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Accessibility : MonoBehaviour
{
    public TMP_Dropdown accessibilityDropdown;

    void Start()
    {
        accessibilityDropdown.onValueChanged.AddListener(ApplyColorScheme);

        int savedIndex = PlayerPrefs.GetInt("ColorScheme", 0);
        accessibilityDropdown.value = savedIndex;
        ApplyColorScheme(savedIndex);
    }

    public void ApplyColorScheme(int index)
    {
        Volume volume = FindObjectOfType<Volume>();
        if (volume != null)
        {
            if (volume != null && volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.saturation.value = (index == 1) ? -100 : 0; // Apply B&W or default color
            }
            else
            {
                Debug.LogError("ColorAdjustments not found on volume profile.");
            }
        }
        else
        {
            Debug.LogError("Volume not found.");
        }

        PlayerPrefs.SetInt("ColorScheme", index);
    }

}
