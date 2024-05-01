using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestGameControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI targetText;
    [SerializeField]
    private TickboxScript[] tickboxes;
    [SerializeField]
    private TextMeshProUGUI healthGainedText;
    private Timer timer;
    private float time;

    private int targetNumber;
    private int currentNumber;

    private GameManager gameManager;
    [SerializeField]
    private int healthPerTickbox;
    private int healthToAdd;

    [SerializeField]
    private GameObject instructionScreen;

    public bool questionInProgress;

    [SerializeField]
    private GameObject maxHPPopupScreen;
    [SerializeField]
    private GameObject dontShowAgainTickbox;
    private bool maxHealthShown = false;

    void Start()
    {
        maxHealthShown = false;
        gameManager = FindObjectOfType<GameManager>();
        timer = FindObjectOfType<Timer>();
        time = timer.GetTimeLeft();
        instructionScreen.SetActive(true);
        if (GameManager.PlayerCurrentHP == GameManager.PlayerMaxHP)
        {
            DisplayMaxHPPopup();
        }
    }

    public void StartNewRound()
    {
        timer.SetTimer(time);
        if (!maxHPPopupScreen.activeSelf)
        {
            timer.StartTimer();
        }
        SetTargetNumber();
        foreach (TickboxScript tickbox in tickboxes)
        {
            tickbox.SetFalse();
        }
        currentNumber = 0;
        healthToAdd = 0;
        questionInProgress = true;
    }

    private void SetTargetNumber()
    {
        targetNumber = Random.Range(1, 128);
        SetTargetText();
    }

    private void SetTargetText()
    {
        targetText.text = targetNumber.ToString();
    }

    public void ChangeCurrent(int value)
    {
        currentNumber += value;
        if (currentNumber == targetNumber)
        {
            AddHealth();
            StartNewRound();
        }
    }

    private void UpdateHealthToAdd()
    {
        foreach (TickboxScript tickbox in tickboxes)
        {
            if (tickbox.active)
            {
                healthToAdd += healthPerTickbox;
            }
        }
    }

    // Adds the health to the player as long as it is less that the max xp a player can have
    private void AddHealth()
    {
        UpdateHealthToAdd();
        if (healthToAdd + GameManager.PlayerCurrentHP >= GameManager.PlayerMaxHP)
        {
            healthToAdd = GameManager.PlayerMaxHP - GameManager.PlayerCurrentHP;
            GameManager.PlayerCurrentHP = GameManager.PlayerMaxHP;
            if (!maxHealthShown)
            {
                DisplayMaxHPPopup();
            }
        }
        GameManager.PlayerCurrentHP += healthToAdd;
        UpdateHealthGainedText(healthToAdd);
        gameManager.DisplayHud();
        healthToAdd = 0;
    }

    // Updates the information about text gained
    private void UpdateHealthGainedText(int value)
    {
        healthGainedText.text = value.ToString();
    }

    public void TimeUp()
    {
        StartNewRound();
    }

    // Displays the pop up that the user has reached the maximum health
    private void DisplayMaxHPPopup()
    {
        timer.PauseTimer();
        maxHPPopupScreen.SetActive(true);
    }

    public void DontShowAgainTickbox()
    {
        maxHealthShown = !maxHealthShown;
        dontShowAgainTickbox.SetActive(!dontShowAgainTickbox.activeSelf);
    }
}