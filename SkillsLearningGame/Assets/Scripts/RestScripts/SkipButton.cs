using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    private RestGameControl restGameControl;
    void Start()
    {
        restGameControl = FindObjectOfType<RestGameControl>();
    }

    // When the skip button is pressed a new round is started
    public void SkipButtonPressed()
    {
        restGameControl.StartNewRound();
    }
}
