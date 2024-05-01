using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectView : MonoBehaviour
{
    [SerializeField]
    private GameObject option1;
    [SerializeField]
    private GameObject option2;
    [SerializeField]
    private GameObject[] option1Objects;
    [SerializeField]
    private GameObject[] option2Objects;
    [SerializeField]
    private Color selectedColor = Color.red;
    [SerializeField]
    private Color unselectedColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        Button1Click();
    }

    // Hides all relevant GameObjects and sets both button colours to black
    private void DeactivateAll()
    {
        foreach (GameObject item in option1Objects)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in option2Objects)
        {
            item.SetActive(false);
        }
        option1.GetComponent<Image>().color = unselectedColor;
        option2.GetComponent<Image>().color = unselectedColor;

    }

    // If button 1 is pressed, hide all GameObjects, then reveal option1Object
    // and set button1's colour
    public void Button1Click()
    {
        DeactivateAll();
        foreach (GameObject item in option1Objects)
        {
            item.SetActive(true);
        }
        option1.GetComponent<Image>().color = selectedColor;
    }

    // If button 2 is pressed, hide all GameObjects, the reveal option2Object
    // and set button2's colour
    public void Button2Click()
    {
        DeactivateAll();
        foreach (GameObject item in option2Objects)
        {
            item.SetActive(true);
        }
        option2.GetComponent<Image>().color = selectedColor;
    }
}
