using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatsLink : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    private string link = "https://skillsbuild.org";

    // Opens the inputted link when clicked
    public void LinkOnClick()
    {
        Application.OpenURL(link);
    }
}