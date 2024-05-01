using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionManager : MonoBehaviour
{
    // Basic attributes for the screen to display properly
    [SerializeField]
    private GameObject selectedCardScreen;
    [SerializeField]
    private SelectedCard selectedCard;

    // To use arrows
    public int selectedID;
    private List<PlayerDeckCards> playerDeckCards = new List<PlayerDeckCards>();
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private GameObject leftArrow;

    public void LoadSelectedCard(Card card)
    {
        selectedCardScreen.SetActive(true);
        selectedCard.DisplaySelectedCard(card);
        playerDeckCards = FindObjectOfType<DeckDisplayManager>().GetPlayerDeckCards();
        SetArrows();
    }

    public void UnloadSelectedCard()
    {
        selectedCardScreen.SetActive(false);
    }

    public void SelectNextCard()
    {
        if (selectedID < playerDeckCards.Count - 1)
        {
            selectedID++;
            LoadSelectedCard(playerDeckCards[selectedID].GetCard());
        }
    }

    public void SelectPreviousCard()
    {
        if (selectedID > 0)
        {
            selectedID--;
            LoadSelectedCard(playerDeckCards[selectedID].GetCard());
        }
    }

    private void SetArrows()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);

        // Hide the right arrow if we're at the last card
        if (selectedID >= playerDeckCards.Count - 1)
        {
            rightArrow.SetActive(false);
        }

        // Hide the left arrow if we're at the first card
        if (selectedID <= 0)
        {
            leftArrow.SetActive(false);
        }
    }
}