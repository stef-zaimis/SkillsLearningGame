using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel;

    // Deactivates the entire statistics UI when the button is pressed.
    public void ButtonClick()
    {
        mainPanel.SetActive(false);
    }
}
