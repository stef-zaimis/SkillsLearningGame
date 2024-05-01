using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubjectsFilter : MonoBehaviour
{
    public Toggle allSubjectsToggle;
    public Toggle[] subjectToggles;
    private bool isUserAction = true; // Flag to distinguish between user actions and programmatic changes

    void Awake()
    {
        allSubjectsToggle.onValueChanged.AddListener(OnAllSubjectsValueChanged);

        foreach (var toggle in subjectToggles)
        {
            toggle.onValueChanged.AddListener(OnSubjectValueChanged);
        }
    }

    private void OnAllSubjectsValueChanged(bool isOn)
    {
        if (isUserAction)
        {
            // If it's a user action, update all subject toggles to match the "All Subjects" state
            foreach (var toggle in subjectToggles)
            {
                toggle.isOn = isOn;
            }
        }
    }

    private void OnSubjectValueChanged(bool isOn)
    {
        if (isUserAction)
        {
            CheckSubjectSelection();
        }
    }

    private void CheckSubjectSelection()
    {
        // Temporarily treat the following programmatic update as not a user action
        isUserAction = false;

        bool allSelected = true;
        foreach (var toggle in subjectToggles)
        {
            if (!toggle.isOn)
            {
                allSelected = false;
                break;
            }
        }

        allSubjectsToggle.isOn = allSelected;

        // Restore isUserAction to true after the programmatic update
        isUserAction = true;
    }
}