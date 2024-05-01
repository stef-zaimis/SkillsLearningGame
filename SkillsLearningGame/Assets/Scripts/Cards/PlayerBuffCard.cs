using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Buff card class
public class PlayerBuffCard : MonoBehaviour, IDropHandler
{
    public GameObject playerHitbox;
    public BattleManager battleManager;

    public bool playCard = false;

    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    // On each frame, set the hitboxes of the target
    void Update()
    {
        if (battleManager.selectedCard != null && battleManager.selectedCard.card.cardTargetType == Card.CardTargetType.Player)
        {
            playerHitbox.SetActive(true);
        }
        else
        {
            playCard = false;
            playerHitbox.SetActive(false);
        }
    }

    // When dropped properly, signal that the card can be played. This is here to block accidental playing of the card when dropped on the wrong target
    public void OnDrop(PointerEventData eventData)
    {
        playCard = true;
    }
}