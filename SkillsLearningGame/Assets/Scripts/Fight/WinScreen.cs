using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is displayed after the player wins a single fight, as an overlay to the fight scene
public class WinScreen : MonoBehaviour
{
    private List<int> displayedCardIndices = new List<int>();
    public CardRewardUI cardRewardButton;
    public List<CardRewardUI> cardRewards;
    GameManager gameManager;
    public GameObject subjectSelectionScreen;
    BattleManager battleManager;
    public GameObject cardRewardsBackground;

    // Get necessary objects and clear the underlying fight UI elements
    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();
        battleManager.clearUIElements();
    }

    // This is called when the player clicks on the button to get a card reward
    public void ChooseSubject()
    {
        subjectSelectionScreen.SetActive(true);
    }

    // This is called if AI is selected
    public void AICards()
    {
        generateRewardCards(Card.CardSubjectEnum.AI);
    }

    // This is called if TI is selected
    public void TICards()
    {
        generateRewardCards(Card.CardSubjectEnum.TI);
    }

    // This is called if cloud is selected
    public void CloudCards()
    {
        generateRewardCards(Card.CardSubjectEnum.Cloud);
    }

    // This is called if DS is selected
    public void DSCards()
    {
        generateRewardCards(Card.CardSubjectEnum.DS);
    }

    // Generate three random cards belonging to the selected subject
    public void generateRewardCards(Card.CardSubjectEnum subject)
    {
        cardRewardsBackground.SetActive(true);
        for (int j = 0; j < 3; j++)
        {
            if (cardRewards[j].gameObject.activeSelf)
            {
                cardRewards[j].gameObject.SetActive(false);
            }
        }
        GameManager.allCards.Shuffle();

        int i = 0;
        int addedCards = 0;
        displayedCardIndices.Clear();

        while (addedCards < 3 && i < GameManager.allCards.Count)
        {
            if (GameManager.allCards[i].cardSubject == subject)
            {
                cardRewards[addedCards].gameObject.SetActive(true);
                cardRewards[addedCards].DisplayCards(GameManager.allCards[i]);
                displayedCardIndices.Add(i);
                addedCards++;
            }
            i++;
        }
    }

    // When a generated card reward is chosen
    public void ChooseCard(int cardID)
    {
        gameManager.SetUpCardToAdd(displayedCardIndices[cardID]);
        for (int i = 0; i < 3; i++)
        {
            cardRewards[i].gameObject.SetActive(false);
        }
    }
}