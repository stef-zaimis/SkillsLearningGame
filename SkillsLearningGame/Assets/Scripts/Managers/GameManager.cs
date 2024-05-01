using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //initial variables passed to manager in unity editor
    public List<Card> initialPlayerDeck = new List<Card>();
    public List<Card> initialAllCards = new List<Card>();

    //static variables we want to persist between scenes
    public static List<Card> playerDeck = new List<Card>();
    public static List<Card> allCards = new List<Card>();
    public static int currentLevel = 0;
    public static int PlayerMaxHP { get; set; }
    public static int PlayerCurrentHP { get; set; }
    public static int MaxEnergy = 3;
    [SerializeField]
    private int initialPlayerHP;
    public static int points = 0;

    // static variables to handle questions
    public static bool answeredCorrectly;
    public static int cardIDToAdd;
    public static Card.CardSubjectEnum cardToAddSubject;
    public static bool letPlayerFilterQuestions = false;
    // static variables to handle player position in the map after quiz battle (depending on failure or pass) if true, the latest node added will be removed (meaning that the player failed the quiz)
    public static bool updateNodes = false;

    public PlayerUI playerUI;

    private SceneManager sceneManager;

    public AudioMixer initialAudioMixer;
    public static AudioMixer audioMixer;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        if (playerDeck.Count == 0)
        {
            InitialiseCards();
        }

        // Set up initial player hp
        if (PlayerMaxHP == 0)
        {
            InitialisePlayerHP();
        }

        Settings.gameManager = this;
        playerUI = FindObjectOfType<PlayerUI>();

        // Display player HUD
        if (playerUI != null)
        {
            DisplayHud();
        }
    }

    void Start()
    {
        SetAudioMixer();
    }

    // Load the player
    public void LoadCharacterUI()
    {
        playerUI.playerUIObject.SetActive(true);
    }

    // Add a card to the player's deck
    public void AddCardToDeck(int cardID)
    {
        if (allCards.Count > cardID)
        {
            playerDeck.Add(allCards[cardID]);
            allCards.Remove(allCards[cardID]);
        }
    }

    /*private void Update()
    {
        currentState.Tick(Time.deltaTime);
    }

    public void SetState(GameState state)
    {
        currentState = state;
    }

    public bool isCurrentState(GameState state)
    {
        if(currentState == state)
        {
            return true;
        }
        return false;
    }*/

    // Display the player HUD
    public void DisplayHud()
    {
        DisplayHP(PlayerCurrentHP, PlayerMaxHP);
        updateLevelText();
        updatePointText();
    }

    // Show the player's HP
    public void DisplayHP(int currentHP, int maxHP)
    {
        playerUI.hpDisplayText.text = $"{currentHP} / {maxHP}";
    }

    // Add initial player cards
    private void InitialiseCards()
    {
        playerDeck = initialPlayerDeck;
        allCards = initialAllCards;
    }

    // Add initial player HP
    private void InitialisePlayerHP()
    {
        PlayerMaxHP = initialPlayerHP;
        PlayerCurrentHP = initialPlayerHP;
    }

    // Increase the current level
    public void increaseLevel()
    {
        currentLevel++;
    }

    // Update the HUD level text
    public void updateLevelText()
    {
        playerUI.levelText.text = "Level " + currentLevel;
    }

    // Update the HUD point text
    public void updatePointText()
    {
        playerUI.pointText.text = points + " pts";
    }

    // Check whether a card should be added (if the question was answered correctly)
    public void evaluateAddingCard()
    {
        if (answeredCorrectly)
        {
            AddCardToDeck(cardIDToAdd);
        }
    }

    // Store the selected card when switching to the question scene
    public void SetUpCardToAdd(int cardID)
    {
        cardIDToAdd = cardID;
        cardToAddSubject = allCards[cardID].cardSubject;
    }

    public void LoadFilteredQuizBattle()
    {
        letPlayerFilterQuestions = true;
        sceneManager.chooseScene("QuizBattle");
    }

    private void SetAudioMixer()
    {
        if (audioMixer == null && initialAudioMixer != null)
        {
            audioMixer = initialAudioMixer;
        }

        if (PlayerPrefs.HasKey("masterVolume") && audioMixer != null)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20);
        }
        else if (audioMixer != null)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(1f) * 20);
        }
    }

    public void ResetAllVariables()
    {
        ResetStats.ResetStatic();
        ResetPlayerVariables();
        ResetCharms();
        MapManager.resetMap = true;
    }

    private void ResetPlayerVariables()
    {
        playerDeck = new List<Card>();
        allCards = new List<Card>();
        currentLevel = 0;
        PlayerMaxHP = 0;
        PlayerCurrentHP = 0;
        MaxEnergy = 3;
        points = 0;
    }

    private void ResetCharms()
    {
        CurrentCharms.OwnedCharms = new Dictionary<string, int>()
        {
            {"Absorption Charm", 0},
            {"Damage Charm", 0},
            {"Energy Charm", 0},
            {"Max Health Charm", 0},
            {"Starter Shield Charm", 0},
            {"Regen Charm", 0},
            {"Hand Size Charm", 0},
        };

        CurrentCharms.CurrentBuff = new Dictionary<string, float>()
        {
            {"Absorption Charm", 1},
            {"Damage Charm", 1},
            {"Energy Charm", 0},
            {"Max Health Charm", 0},
            {"Starter Shield Charm", 0},
            {"Regen Charm", 0},
            {"Hand Size Charm", 0},
        };

        CurrentCharms.CharmSign = new Dictionary<string, string>()
        {
            {"Absorption Charm", "÷"},
            {"Damage Charm", "x"},
            {"Energy Charm", "+"},
            {"Max Health Charm", "+"},
            {"Starter Shield Charm", "+"},
            {"Regen Charm", "+"},
            {"Hand Size Charm", "+"}
        };
    }
}