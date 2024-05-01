using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedCard : MonoBehaviour
{
    // Basic attributes
    public Image selectedCardImage;
    public TMP_Text selectedCardName;
    public TMP_Text selectedCardDescription;
    public TMP_Text selectedCardType;
    public TMP_Text selectedCardSubject;
    public TMP_Text selectedCardCost;

    public void DisplaySelectedCard(Card selectedCard)
    {
        selectedCardImage.sprite = selectedCard.cardImage;
        selectedCardName.text = selectedCard.cardName;
        selectedCardDescription.text = selectedCard.cardDescription;
        selectedCardSubject.text = selectedCard.cardSubject.ToString();
        selectedCardCost.text = selectedCard.cardCost.ToString();
        selectedCardType.text = selectedCard.cardTypeEnum.ToString();
    }
}
