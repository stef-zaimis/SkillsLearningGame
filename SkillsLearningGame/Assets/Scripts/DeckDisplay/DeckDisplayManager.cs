using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckDisplayManager : MonoBehaviour
{
    private List<PlayerDeckCards> playerCardsUI;
    public GameObject playerDeckCardPrefab;
    private List<Card> playerCards;
    public Transform parentTransform;
    private ScrollRect scrollRect;

    void Start()
    {
        playerCards = GameManager.playerDeck;
        playerCardsUI = new List<PlayerDeckCards>();
        ScrollRect scrollRect = FindObjectOfType<ScrollRect>();
        if (scrollRect != null)
        {
            ScrollToTop(scrollRect);
        }
        DisplayDeck();
    }

    public void DisplayDeck()
    {
        foreach (var card in playerCards)
        {
            // Instantiate the current card prefab under the proper parent
            GameObject playerDeckCardObject = Instantiate(playerDeckCardPrefab, parentTransform);

            // Get the playerdeckcard component from the prefab
            PlayerDeckCards playerDeckCard = playerDeckCardObject.GetComponent<PlayerDeckCards>();

            if (card != null)
            {
                playerDeckCard.DisplayPlayerCard(card);
                playerDeckCard.gameObject.SetActive(true);

                playerCardsUI.Add(playerDeckCard);
            }
        }

        // Scroll to top
        if (scrollRect != null)
        {
            ScrollToTop(scrollRect);
        }
    }

    public List<PlayerDeckCards> GetPlayerDeckCards()
    {
        return playerCardsUI;
    }

    public void ScrollToTop(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public void ReturnButton()
    {
        Debug.Log("Returning");
        // Logic to go back here, since this is all placeholder and will probably be scene overlays I'll leave it blank
    }
}
