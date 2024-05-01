using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mainPanel;
    [SerializeField]
    private List<GameObject> hoverPanel;

    // Start is called before the first frame update
    void Start()
    {
        Exit();
    }

    // Entering into the area, show the information in the hover panel
    public void Enter()
    {
        foreach (GameObject panel in mainPanel)
        {
            panel.SetActive(false);
        }
        foreach (GameObject panel in hoverPanel)
        {
            panel.SetActive(true);
        }
    }

    // Exitting the area, ensure the information in the main panel is shown
    public void Exit()
    {
        foreach (GameObject panel in mainPanel)
        {
            panel.SetActive(true);
        }
        foreach (GameObject panel in hoverPanel)
        {
            panel.SetActive(false);
        }
    }
}
