using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopCards : MonoBehaviour
{
    public Image shopCardImage;
    public TMP_Text shopCardName;
    public TMP_Text shopCardDescription;
    public TMP_Text shopCardType;
    public TMP_Text shopCardSubject;
    public TMP_Text shopCardCost;
    public TMP_Text shopCardPointCost;
    public TMP_Text quantityText;
    public int quantity;
    public int cost;

    public void DisplayShopCard(Card shopCard)
    {
        shopCardImage.sprite = shopCard.cardImage;
        shopCardName.text = shopCard.cardName;
        shopCardDescription.text = shopCard.cardDescription;
        shopCardSubject.text = shopCard.cardSubject.ToString();
        shopCardCost.text = shopCard.cardCost.ToString();
        shopCardType.text = shopCard.cardTypeEnum.ToString();
    }
}