using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// The UI displayed of our cards
public class CardUI : MonoBehaviour
{
    public Card card;
    public CardUIProperties[] properties;
    public TMP_Text cardNameText;
    public TMP_Text cardDescriptionText;
    public TMP_Text cardCostText;
    public TMP_Text cardTypeText;
    public TMP_Text cardSubjectText;
    public GameObject discardEffect;
    public Image cardImage;

    //to make card appear over others when hovered over
    private Canvas tempCanvas;
    private GraphicRaycaster tempRaycaster;

    private PlayerBuffCard playerBuffCard;

    BattleManager battleManager;

    public void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        playerBuffCard = FindObjectOfType<PlayerBuffCard>();
    }

    // Reset your size when not hovered over or selected
    private void OnEnable()
    {
        ResetSize();
    }

    // Load a card
    public void LoadCard(Card c)
    {
        if (c == null)
        {
            return;
        }

        card = c;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        cardNameText.text = card.cardName;
        cardDescriptionText.text = card.cardDescription;
        cardCostText.text = card.cardCost.ToString(); ;
        cardImage.sprite = card.cardImage;
        cardSubjectText.text = card.cardSubject.ToString();
        cardTypeText.text = card.cardTypeEnum.ToString();
        /*
        for (int i = 0; i<c.properties.Length; i++)
        {
            CardProperties cardProps = c.properties[i];

            CardUIProperties cardUIProps = GetProperty(cardProps.element);

            if(cardUIProps == null)
            {
                continue;
            }

            if(cardProps.element is ElementInt)
            {
                cardUIProps.text.text = cardProps.intValue.ToString();
            }

            if (cardProps.element is ElementText)
            {
                cardUIProps.text.text = cardProps.stringValue;
            }
            else
            {
                if (cardProps.element is ElementImage)
                {
                    cardUIProps.image.sprite = cardProps.sprite;
                }
            }
        }
        */
    }

    /*public CardUIProperties GetProperty(Element e)
    {
        CardUIProperties result = null;
        for(int i=0; i<properties.Length; i++)
        {
            if (properties[i].element == e)
            {
                result = properties[i];
                break;
            }
        }
        return result;
    }*/

    // Select a card when clicked on and dragged
    public void SelectCard()
    {
        battleManager.selectedCard = this;
    }

    // Deselect card when the mouse is released
    public void DeselectCard()
    {
        battleManager.selectedCard = null;
        ResetSize();
    }

    // If a card is dropped, reset its size
    public void DropCard()
    {
        if (battleManager.selectedCard == null)
        {
            ResetSize();
        }
    }

    // When hovering over a card
    public void HoverCard()
    {
        if (battleManager.selectedCard == null)
        {
            Magnify();
        }

    }

    // This is called if you are hovering over a card
    public void Magnify()
    {
        // Make hte card bigger
        this.transform.localScale = new Vector3(1.4F, 1.4F, 1.4F);

        // Add the card to a new, temporary canvas, to make it appear over the other cards
        tempCanvas = gameObject.AddComponent<Canvas>();
        tempCanvas.overrideSorting = true;
        tempCanvas.sortingOrder = 1;
        tempRaycaster = gameObject.AddComponent<GraphicRaycaster>();
    }

    // Reset the cards size and canvas
    public void ResetSize()
    {
        Vector3 s = Vector3.one;
        this.transform.localScale = s;
        Destroy(tempRaycaster);
        Destroy(tempCanvas);
    }

    // When the drag is ended and the card is dropped
    public void HandleEndDrag()
    {
        // If the player doesnt have enough energy, dont play the card
        if (battleManager.energyCount < card.cardCost)
        {
            Debug.Log("Not enough energy");
            return;
        }

        // If the card is an attack card and it's not dropped on the enemy
        if (card.cardTargetType == Card.CardTargetType.Enemy && battleManager.cardTarget == null)
        {
            Debug.Log("Not dropped on an enemy");
            return;
        }

        // If the card is dropped on the correct area, play it, else don't
        if (card.cardTargetType == Card.CardTargetType.Enemy)
        {
            StatisticValues.CardsUsed += 1;
            battleManager.PlayCard(this);
            ResetSize();
            playerBuffCard.playCard = false;
        }
        else if (card.cardTargetType == Card.CardTargetType.Player && playerBuffCard.playCard)
        {
            StatisticValues.CardsUsed += 1;
            battleManager.PlayCard(this);
            ResetSize();
            playerBuffCard.playCard = false;
        }
    }
}
