using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject popupScreen;
    void Start()
    {
        HidePopup();
    }

    public void ShowPopup()
    {
        popupScreen.SetActive(true);
    }

    public void HidePopup()
    {
        popupScreen.SetActive(false);
    }
}
