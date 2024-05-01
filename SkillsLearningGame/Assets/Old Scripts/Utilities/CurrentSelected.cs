using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSelected : MonoBehaviour
{
    public CardVariable currentCard;
    public CardUI cardUI;

    Transform mTransform;
    /*
    public void LoadCard()
    {
        if(currentCard == null)
        {
            return;
        }
        currentCard.value.gameObject.SetActive(false);
        cardUI.LoadCard(currentCard.value.ui.card);
        cardUI.gameObject.SetActive(true);
    }

    public void CloseCard()
    {
        cardUI.gameObject.SetActive(false);
    }
    
    public void Start()
    {
        mTransform = this.transform;
        CloseCard();
    }

    void Update()
    {
       mTransform.position = Input.mousePosition;
    }*/
}
