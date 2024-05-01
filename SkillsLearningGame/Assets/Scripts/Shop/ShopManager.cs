using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Numerics;

public class ShopManager : MonoBehaviour
{
    public bool inCharmSection;
    private GameManager gameManager;
    [SerializeField]
    private CharmEffect charmEffect;
    [SerializeField]
    private GameObject lackOfFundsPopup;
    [SerializeField]
    private MaxCharmsReached maxCharmsReachedPopup;
    [SerializeField]
    private GameObject fallingNotificationPrefab;

    [Header("Shop Charms")]
    public CharmItem[] shopCharms;
    public GameObject[] shopCharmPanelsGO;
    public ShopTemplate[] shopCharmPanels;
    public UnityEngine.UI.Button[] charmPurchaseButtons;

    [Header("Shop Cards")]
    public Dictionary<string, int> cardInventory = new Dictionary<string, int>();
    public List<ShopCards> shopCards;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.DisplayHud();
        if (inCharmSection)
        {
            for (int i = 0; i < shopCharms.Length; i++)
            {
                shopCharmPanelsGO[i].SetActive(true);
            }
            LoadCharms();
        }
        else
        {
            LoadShopCards();
        }
    }

    public void LoadShopCards()
    {
        if (cardInventory.Count <= 0)
        {
            InitialiseCardInventory();
        }
        DisplayShopCards();
    }

    public void InitialiseCardInventory()
    {
        List<Card> sortedCards = new List<Card>(GameManager.allCards);
        sortedCards.Sort((card1, card2) => card1.cardName.CompareTo(card2.cardName));
        cardInventory.Clear();
        foreach (Card card in sortedCards)
        {
            if (!cardInventory.ContainsKey(card.cardName))
            {
                cardInventory.Add(card.cardName, 1);
            }
            else
            {
                cardInventory[card.cardName]++;
            }
        }
    }

    public void LoadCharms()
    {
        if (inCharmSection)
        {
            for (int i = 0; i < shopCharms.Length; i++)
            {
                shopCharmPanels[i].name.text = shopCharms[i].name;
                shopCharmPanels[i].description.text = shopCharms[i].description;
                shopCharmPanels[i].cost.text = shopCharms[i].cost.ToString() + " pts";
            }
        }
    }

    public void PurchaseCharm(int buttonID)
    {
        if (inCharmSection)
        {
            int currentlyOwned = CurrentCharms.OwnedCharms[shopCharms[buttonID].name];
            if (shopCharms[buttonID].maxAllowed != -1 && shopCharms[buttonID].maxAllowed <= currentlyOwned)
            {
                maxCharmsReachedPopup.ActivatePopup(shopCharms[buttonID].name);
            }
            else if (GameManager.points >= shopCharms[buttonID].cost)
            {
                GameManager.points -= shopCharms[buttonID].cost;
                gameManager.DisplayHud();
                SpendMoney(shopCharms[buttonID].cost);
                charmEffect.UseCharm(shopCharms[buttonID].name);
            }
            else
            {
                lackOfFundsPopup.SetActive(true);
            }
        }
    }

    public void DisplayShopCards()
    {
        int processedCards = 0;
        foreach (var entry in cardInventory)
        {
            if (processedCards < shopCards.Count)
            {
                Card cardToDisplay = GameManager.allCards.Find(card => card.cardName == entry.Key);
                if (cardToDisplay != null)
                {
                    shopCards[processedCards].DisplayShopCard(cardToDisplay);
                    shopCards[processedCards].quantity = entry.Value;
                    shopCards[processedCards].quantityText.text = entry.Value.ToString();
                    shopCards[processedCards].cost = cardToDisplay.cardCost * 10;
                    shopCards[processedCards].shopCardPointCost.text = "Cost: " + (cardToDisplay.cardCost * 10).ToString() + " pts";
                    shopCards[processedCards].gameObject.SetActive(true);
                    processedCards++;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void PurchaseCard(int cardID)
    {
        if (!inCharmSection)
        {
            ShopCards selectedShopCard = shopCards[cardID];
            if (GameManager.points >= selectedShopCard.cost)
            {
                GameManager.points -= selectedShopCard.cost;
                gameManager.AddCardToDeck(GameManager.allCards.FindIndex(card => card.cardName == selectedShopCard.shopCardName.text));
                foreach (ShopCards shopCard in shopCards)
                {
                    shopCard.gameObject.SetActive(false);
                }
                cardInventory.Clear();
                InitialiseCardInventory();
                DisplayShopCards();
                gameManager.DisplayHud();
                SpendMoney(selectedShopCard.cost);
            }
            else
            {
                lackOfFundsPopup.SetActive(true);
            }
        }
    }

    private void SpendMoney(int amount)
    {
        UnityEngine.Vector2 pos = new UnityEngine.Vector2(569, 420);
        Color color = new Color(241 / 255f, 196 / 255f, 15 / 255f);
        var obj = Instantiate(fallingNotificationPrefab, pos, UnityEngine.Quaternion.identity);
        obj.transform.SetParent(transform.parent);
        var message = obj.GetComponent<FallingNotification>();
        message.SetText("-" + amount.ToString() + " pts", color);
    }
}