using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryScript : MonoBehaviour
{

    [SerializeField]
    private GameObject[] images;

    void Start()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
    }

    public void setCategory(string category)
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
            if (image.name == category)
            {
                image.SetActive(true);
            }
        }
    }
}
