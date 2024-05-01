using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TickboxScript : MonoBehaviour
{
    [SerializeField]
    private GameObject tick;
    [SerializeField]
    private int value;
    [SerializeField]
    private RestGameControl restGameControl;
    public bool active = false;
    [SerializeField]
    private TMP_Text tickboxValueText;


    // Start is called before the first frame update
    // All the items are initially set to false
    void Start()
    {
        SetFalse();
    }

    // The tick image is hidden, and the bool related to the tickbox is also set to false
    public void SetFalse()
    {
        tick.SetActive(false);
        active = false;
        UpdateText();
    }

    // Called when the tickbox is clicked
    // Tickbox shown if its hidden and vice versa and the current number in restGameControl is changed
    // depending on the corresponding value for the tickbox
    public void onClick()
    {
        active = !active;
        tick.SetActive(active);
        if (active)
        {
            restGameControl.ChangeCurrent(value);
        }
        else
        {
            restGameControl.ChangeCurrent(-1 * value);
        }
        UpdateText();
    }

    // Text is updated depending on whether the tickbox is active or not.
    private void UpdateText()
    {
        if (active)
        {
            tickboxValueText.text = "1";
        }
        else
        {
            tickboxValueText.text = "0";
        }
    }
}