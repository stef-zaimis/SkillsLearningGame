using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    public bool inThisSection;

    public void Awake()
    {
        updateColor();
    }

    public void updateColor()
    {
        if (inThisSection)
        {
            buttonText.color = Color.gray;
        }
        else
        {
            buttonText.color = Color.black;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!inThisSection)
        {
            buttonText.color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!inThisSection)
        {
            buttonText.color = Color.black;
        }
    }
}