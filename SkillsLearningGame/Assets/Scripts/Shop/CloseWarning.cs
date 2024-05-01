using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWarning : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toClose;
    [SerializeField]
    private GameObject[] toOpen;

    // Hides all Game Objects in toClose and shows all Game Objects in toOpen when button clicked.
    public void OnClick()
    {
        foreach (GameObject item in toClose)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in toOpen)
        {
            item.SetActive(true);
        }
    }
}
