using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> deck;
    public List<Card> drawPile = new List<Card>();
    public List<Card> handCards = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public CardUI selectedCard;
    public List<CardUI> handCardGameObjects = new List<CardUI>();

    [Header("Stats")]
    public Entity cardTarget;
    public Entity player;
    public int energyCount;
    public int cardsToDraw = 5;
    public Turn turn;
    public enum Turn { Player, Enemy };

    [Header("UI")]
    public GameObject endTurnButton;
    [SerializeField]
    private GameObject deckView;
    [SerializeField]
    private GameObject handObject;
    public TMP_Text drawPileCountText;
    public TMP_Text discardPileCountText;
    public TMP_Text energyText;
    public Transform topParent;
    public Transform enemyParent;
    public WinScreen winScreen;

    [Header("Enemies")]
    public List<Enemy> enemies = new List<Enemy>();
    List<Entity> enemyEntitiese = new List<Entity>();
    public GameObject[] possibleEnemies;
    public GameObject[] possibleBosses;
    bool bossFight;
    CardActions cardActions;
    GameManager gameManager;
    SceneManager sceneManager;
    public Animator banner;
    public TMP_Text turnText;
    public GameObject gameover;

    // Upon starting the battle set the player turn and the fight type
    private void Awake()
    {
        turn = Turn.Player;
        gameManager = FindObjectOfType<GameManager>();
        sceneManager = FindObjectOfType<SceneManager>();
        cardActions = GetComponent<CardActions>();
        cardsToDraw += (int)CurrentCharms.CurrentBuff["Hand Size Charm"];
        if (Levels.level.levelType == Level.Type.fight)
        {
            sceneManager.StartBattle("fight");
        }
        else if (Levels.level.levelType == Level.Type.bossFight)
        {
            sceneManager.StartBattle("bossFight");
        }
    }

    // Start the fight
    public void BeginFight()
    {
        StartFight(possibleEnemies);
    }

    // Start a boss fight
    public void BeginBossFight()
    {
        bossFight = true;
        StartFight(possibleBosses);
    }

    // During the player's turn
    private void PlayerTurn()
    {
        // Display each enemy's next turn action
        foreach (Enemy e in enemies)
        {
            e.DisplayIntent();
        }

        turn = Turn.Player;

        // reset block
        player.block = 0;
        player.entityHPBar.ShowBlock(0);

        // Reset energy
        energyCount = GameManager.MaxEnergy;
        energyText.text = energyCount.ToString();

        // Display the end turn button and draw player cards
        ShowOrHideTurnButton();
        DrawCards(cardsToDraw);

        // Display the player's turn 
        turnText.text = "Player's Turn";
        banner.Play("bannerOut");
    }

    // Initial Battle Setup
    public void StartFight(GameObject[] prefabsArray)
    {
        gameManager.updateLevelText();
        gameManager.updatePointText();

        // Text to indicate turn
        turnText.text = "Player's Turn";
        banner.Play("bannerOut");

        // Enemy initial setup
        GameObject newEnemy = Instantiate(prefabsArray[Random.Range(0, prefabsArray.Length)], enemyParent);
        newEnemy.SetActive(true);
        if (winScreen != null)
        {
            winScreen.gameObject.SetActive(false);
        }

        Enemy[] eArr = FindObjectsOfType<Enemy>();
        enemies = new List<Enemy>();

        foreach (Enemy e in eArr) { enemies.Add(e); }
        foreach (Enemy e in eArr) { enemyEntitiese.Add(e.GetComponent<Entity>()); }
        foreach (Enemy e in eArr) { e.DisplayIntent(); }

        // player deck setup
        foreach (Card card in handCards)
        {
            Discard(card);
        }
        foreach (CardUI cardUI in handCardGameObjects)
        {
            cardUI.gameObject.SetActive(false);
        }

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        handCards = new List<Card>();

        discardPile.AddRange(GameManager.playerDeck);
        Shuffle();
        DrawCards(cardsToDraw);
        energyCount = GameManager.MaxEnergy;
        player.block = Mathf.RoundToInt(CurrentCharms.CurrentBuff["Starter Shield Charm"]);
        energyText.text = energyCount.ToString();
    }

    public void OnEndTurnButtonPressed()
    {
        if (turn == Turn.Player)
        {
            clearUIElements();

            // Retrieve non-null enemies
            foreach (Enemy e in enemies)
            {
                if (e.currentEnemy == null)
                    e.currentEnemy = e.GetComponent<Entity>();

                // reset enemy block
                e.currentEnemy.block = 0;
                e.currentEnemy.entityHPBar.ShowBlock(0);
            }

            player.UpdateBuffs();
            // Start enemy turn
            turn = Turn.Enemy;
            StartCoroutine(EnemyTurn());
        }
    }

    // Shuffle the cards to draw
    public void Shuffle()
    {
        discardPile.Shuffle();
        drawPile = discardPile;
        discardPile = new List<Card>();
        discardPileCountText.text = discardPile.Count.ToString();
    }

    // Draw a given amount of cards
    public void DrawCards(int amountToDraw)
    {
        if (amountToDraw > GameManager.playerDeck.Count)
        {
            amountToDraw = GameManager.playerDeck.Count;
        }

        int cardsDrawn = 0;
        while (cardsDrawn < amountToDraw && handCards.Count <= 15)
        {
            if (drawPile.Count < 1)
                Shuffle();

            handCards.Add(drawPile[0]);
            DisplayCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
        int spacing = -1 * (50 + (handCards.Count - 5) * 15);
        if (handCards.Count >= 10)
        {
            handObject.GetComponent<HorizontalLayoutGroup>().spacing = -140 - (handCards.Count - 10) * 4;
        }
        else
        {
            handObject.GetComponent<HorizontalLayoutGroup>().spacing = spacing;
        }
    }

    // Display a card in the players hand
    public void DisplayCardInHand(Card card)
    {
        CardUI cardUI = handCardGameObjects[handCards.Count - 1];
        cardUI.LoadCard(card);
        cardUI.gameObject.SetActive(true);
    }

    // When a card is properly dropped, play it and remove the corresponding energy and card from the player's current attributes
    public void PlayCard(CardUI cardUI)
    {
        cardActions.Action(cardUI.card, cardTarget);

        energyCount -= cardUI.card.cardCost;
        energyText.text = energyCount.ToString();

        selectedCard = null;
        cardUI.gameObject.SetActive(false);
        handCards.Remove(cardUI.card);
        Discard(cardUI.card);
    }

    // Add a played card to the discard pile
    public void Discard(Card card)
    {
        discardPile.Add(card);
        discardPileCountText.text = discardPile.Count.ToString();
    }

    // On enemy turn
    private IEnumerator EnemyTurn()
    {
        // Display enemy's turn banner
        turnText.text = "Enemy's Turn";
        banner.Play("bannerIn");

        yield return new WaitForSeconds(1.5f);

        // Hide each enemy's intent and play their turns
        foreach (Enemy enemy in enemies)
        {
            enemy.HideIntent();
            enemy.midTurn = true;
            enemy.PlayTurn();
            while (enemy.midTurn)
                yield return new WaitForEndOfFrame();
        }
        turn = Turn.Player;
        PlayerTurn();
    }

    // At the end of the fight evaluate win ro loss
    public void FightEnd(bool win)
    {
        if (!win)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            player.Reset();
            WinScreen();
        }
    }

    // If the game is won display the win/rewards screen
    public void WinScreen()
    {
        winScreen.gameObject.SetActive(true);
    }

    // Clear the fight UI elements on win screen overlay
    public void clearUIElements()
    {
        endTurnButton.gameObject.SetActive(false);

        // discard player hand
        foreach (Card card in handCards)
        {
            Discard(card);
        }
        foreach (CardUI cardUI in handCardGameObjects)
        {
            if (cardUI.gameObject.activeSelf)
                cardUI.gameObject.SetActive(false);
            handCards.Remove(cardUI.card);
        }
    }

    public void ShowOrHideTurnButton()
    {
        if (endTurnButton != null && deckView.activeSelf)
        {
            endTurnButton.SetActive(false);
        }
        else if (!deckView.activeSelf && turn == Turn.Player && !winScreen.gameObject.activeSelf)
        {
            endTurnButton.SetActive(true);
        }
    }
}