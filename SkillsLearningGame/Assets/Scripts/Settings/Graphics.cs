using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graphics : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;

    void Start()
    {
        InitializeQualityDropdown();
        SetCurrentQualitySetting();
    }

    private void InitializeQualityDropdown()
    {
        graphicsDropdown.ClearOptions();
        List<string> qualityNames = new List<string>(QualitySettings.names);
        graphicsDropdown.AddOptions(qualityNames);
        graphicsDropdown.onValueChanged.AddListener(SetQuality);
    }

    private void SetCurrentQualitySetting()
    {
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        graphicsDropdown.value = qualityIndex;
        graphicsDropdown.RefreshShownValue();
    }
}
