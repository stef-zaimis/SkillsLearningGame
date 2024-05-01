using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Map;

// The player HUD class
public class PlayerUI : MonoBehaviour
{
    public TMP_Text hpDisplayText;
    public TMP_Text levelText;
    public TMP_Text pointText;
    public GameObject playerUIObject;
    GameManager gameManager;
    ScrollNonUI mapScroll;
    [SerializeField]
    private GameObject deckView;
    [SerializeField]
    private GameObject statsView;
    [SerializeField]
    private GameObject instructionView;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DeckButton()
    {
        instructionView.SetActive(false);
        statsView.SetActive(false);
        mapScroll = FindObjectOfType<ScrollNonUI>();
        if (mapScroll != null)
        {
            mapScroll.activeOverlay = !deckView.activeSelf;
        }
        deckView.SetActive(!deckView.activeSelf);
    }

    public void StatsButton()
    {
        instructionView.SetActive(false);
        deckView.SetActive(false);
        mapScroll = FindObjectOfType<ScrollNonUI>();
        if (mapScroll != null)
        {
            mapScroll.activeOverlay = !statsView.activeSelf;
        }
        statsView.SetActive(!statsView.activeSelf);
    }

    public void InstructionButton()
    {
        statsView.SetActive(false);
        deckView.SetActive(false);
        mapScroll = FindObjectOfType<ScrollNonUI>();
        if (mapScroll != null)
        {
            mapScroll.activeOverlay = !instructionView.activeSelf;
        }
        instructionView.SetActive(!instructionView.activeSelf);
    }
}