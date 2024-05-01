using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSelectedCardHandler : MonoBehaviour
{
    private CardSelectionManager cardSelectionManager;
    public GameObject selectedCardScreen;

    void Start()
    {
        cardSelectionManager = FindObjectOfType<CardSelectionManager>();
    }

    void Update()
    {
        // Check if the selected card screen is active
        if (selectedCardScreen.activeSelf)
        {
            // Check for keyboard input
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Call the SelectNextCard method
                cardSelectionManager.SelectNextCard();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Call the SelectPreviousCard method
                cardSelectionManager.SelectPreviousCard();
            }
        }
    }
}
