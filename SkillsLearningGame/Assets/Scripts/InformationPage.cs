using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InformationPage : MonoBehaviour
{
    [SerializeField]
    private Color unselectedColor = Color.white;
    [SerializeField]
    public GameObject Button1;
    [SerializeField]
    public GameObject Button2;
    [SerializeField]
    public GameObject Button3;
    [SerializeField]
    public GameObject Button4;
    [SerializeField]
    public GameObject Content1;
    [SerializeField]
    public GameObject Content2;
    [SerializeField]
    public GameObject Content3;
    [SerializeField]
    public GameObject Content4;
    [SerializeField]
    public GameObject ExitButton;
    [SerializeField]
    private GameObject MainPanel;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAll();
        Button1Press();
    }

    void DeactivateAll()
    {
        Content1.SetActive(false);
        Content2.SetActive(false);
        Content3.SetActive(false);
        Content4.SetActive(false);
        Button1.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        Button2.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        Button3.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        Button4.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
    }

    public void Button1Press()
    {
        DeactivateAll();
        Content1.SetActive(true);
        Color redColor = new Color(236f / 255f, 81f / 255f, 81f / 255f);
        Button1.GetComponent<UnityEngine.UI.Image>().color = redColor;
    }

    public void Button2Press()
    {
        DeactivateAll();
        Content2.SetActive(true);
        Color purpleColor = new Color(118f / 255f, 0f, 137f / 255f);
        Button2.GetComponent<UnityEngine.UI.Image>().color = purpleColor;
    }

    public void Button3Press()
    {
        DeactivateAll();
        Content3.SetActive(true);
        Color greenColor = new Color(0f, 111f / 255f, 5f / 255f);
        Button3.GetComponent<UnityEngine.UI.Image>().color = greenColor;
    }

    public void Button4Press()
    {
        DeactivateAll();
        Content4.SetActive(true);
        Color blueColor = new Color(0f, 63f / 255f, 111f / 255f);
        Button4.GetComponent<UnityEngine.UI.Image>().color = blueColor;
    }

    public void ExitButtonPress()
    {
        MainPanel.SetActive(false);
    }
}
