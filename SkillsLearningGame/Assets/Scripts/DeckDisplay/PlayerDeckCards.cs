using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerDeckCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Basic attributes
    public Image playerCardImage;
    public TMP_Text playerCardName;
    public TMP_Text playerCardDescription;
    public TMP_Text playerCardType;
    public TMP_Text playerCardSubject;
    public TMP_Text playerCardCost;

    // To handle selecting a card
    private Card thisCard;
    private CardSelectionManager cardSelectionManager;
    private DeckDisplayManager deckDisplayManager;

    // For the hover over effect
    private Vector3 originalScale;
    private Canvas tempCanvas;
    private GraphicRaycaster tempRaycaster;

    void Start()
    {
        originalScale = transform.localScale;
        cardSelectionManager = FindObjectOfType<CardSelectionManager>();
        deckDisplayManager = FindObjectOfType<DeckDisplayManager>();
    }

    public void DisplayPlayerCard(Card playerCard)
    {
        thisCard = playerCard;
        playerCardImage.sprite = playerCard.cardImage;
        playerCardName.text = playerCard.cardName;
        playerCardDescription.text = playerCard.cardDescription;
        playerCardSubject.text = playerCard.cardSubject.ToString();
        playerCardCost.text = playerCard.cardCost.ToString();
        playerCardType.text = playerCard.cardTypeEnum.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // add and configure necessary components to make the card appear on top of the other cards
        //tempCanvas = gameObject.AddComponent<Canvas>();
        //tempCanvas.overrideSorting = true;
        //tempCanvas.sortingOrder = 1;
        //tempRaycaster = gameObject.AddComponent<GraphicRaycaster>();

        // Increase the scale of the card by 20% when the mouse pointer enters
        this.transform.localScale = originalScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetSize();
    }

    public void ResetSize()
    {
        // Restore the original scale of the card when the mouse pointer exits
        this.transform.localScale = originalScale;

        // Destroy added components
        //Destroy(tempRaycaster);
        //Destroy(tempCanvas);
    }

    public void ClickedOn()
    {
        ResetSize();

        // Set the ID of the selected card so that we can navigate to the neighbouring ones
        cardSelectionManager.selectedID = deckDisplayManager.GetPlayerDeckCards().IndexOf(this);

        cardSelectionManager.LoadSelectedCard(thisCard);
    }

    public Card GetCard()
    {
        return thisCard;
    }
}