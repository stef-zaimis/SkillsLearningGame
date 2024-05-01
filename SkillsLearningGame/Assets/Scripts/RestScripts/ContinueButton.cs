using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionPanel;
    [SerializeField]
    private GameObject gamePanel;
    private Timer timer;
    private RestGameControl restGameControl;
    [SerializeField]
    private TextMeshProUGUI startButtonText;
    [SerializeField]
    private GameObject maxHPPanel;

    void Start()
    {
        instructionPanel.SetActive(true);
        //gamePanel.SetActive(false);
        timer = FindObjectOfType<Timer>();
        restGameControl = FindObjectOfType<RestGameControl>();
        SetUpUIElements();
    }

    void OnEnable()
    {
        SetUpUIElements();
    }

    public void SetUpUIElements()
    {
        if (restGameControl != null && restGameControl.questionInProgress && startButtonText != null)
        {
            startButtonText.text = "Continue";
        }
    }

    public void StartButtonPress()
    {
        instructionPanel.SetActive(false);
        //gamePanel.SetActive(true);
        if (!restGameControl.questionInProgress)
        {
            restGameControl.StartNewRound();
        }
        timer.StartTimer();
    }

    public void ContinueButtonPressed()
    {
        maxHPPanel.SetActive(false);
        if (!instructionPanel.activeSelf)
        {
            timer.StartTimer();
        }
    }
}
