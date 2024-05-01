using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// The card reward class is a set of displayed cards shown when the player selects a card reward. They act as buttons that redirect you to the quesiton scene to add a card to your deck
public class CardRewardUI : MonoBehaviour
{
    public Image cardRewardImage;
    public TMP_Text cardRewardName;
    public TMP_Text cardRewardDescription;
    public TMP_Text cardRewardType;
    public TMP_Text cardRewardSubject;
    public TMP_Text cardRewardCost;

    // This displays a given reward card
    public void DisplayCards(Card rewardCard)
    {
        cardRewardImage.sprite = rewardCard.cardImage;
        cardRewardName.text = rewardCard.cardName;
        cardRewardDescription.text = rewardCard.cardDescription;
        cardRewardSubject.text = rewardCard.cardSubject.ToString();
        cardRewardCost.text = rewardCard.cardCost.ToString();
        cardRewardType.text = rewardCard.cardTypeEnum.ToString();
    }
}
