using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinkScript : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    private string link = "https://skillsbuild.org";


    public void LinkOnClick()
    {
        Application.OpenURL(link);
    }

    public void SetLink(string newLink)
    {
        link = newLink;
    }
}
