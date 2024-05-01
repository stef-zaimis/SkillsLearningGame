using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic for the cards to only work if dropped on the proper target
public class CardTarget : MonoBehaviour
{
    BattleManager battleManager;
    Entity enemyEntity;
    public GameObject hitbox;

    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        enemyEntity = GetComponent<Entity>();
    }

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        enemyEntity = GetComponent<Entity>();
    }

    // On each frame, if a card is selected and getting dragged, display the correct hitbox to drop it on
    private void Update()
    {
        if (battleManager.selectedCard != null && battleManager.selectedCard.card.cardTargetType == Card.CardTargetType.Enemy)
        {
            showHitbox();
        }
        else
        {
            hideHitbox();
        }
    }

    // If mouse is over the correct area
    public void MouseOver()
    {
        if (enemyEntity == null)
        {
            Debug.Log("no enemy");
            battleManager = FindObjectOfType<BattleManager>();
            enemyEntity = GetComponent<Entity>();
        }

        if (battleManager.selectedCard != null && battleManager.selectedCard.card.cardTypeEnum == Card.CardTypeEnum.Attack)
        {
            battleManager.cardTarget = enemyEntity;
        }
    }

    // Show the target hitbox
    public void showHitbox()
    {
        hitbox.SetActive(true);
    }

    // Hide the target hitbox
    public void hideHitbox()
    {
        hitbox.SetActive(false);
    }

    // On mouse exiting the hitbox
    public void MouseExit()
    {
        battleManager.cardTarget = null;
    }
}