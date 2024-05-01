using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionPanel;
    [SerializeField]
    private GameObject gamePanel;
    private Timer timer;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    // When the instruction button is pressed the instruction panel is shown
    public void InstructionButtonPress()
    {
        timer.PauseTimer();
        instructionPanel.SetActive(true);
        //gamePanel.SetActive(false);
    }
}
