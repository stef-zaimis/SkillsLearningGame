using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// These are all the actions that can be performed by a card
public class CardActions : MonoBehaviour
{
    Card card;

    public Entity target;
    public Entity user;

    BattleManager battleManager;

    // Get an instance of the battle manager upon waking
    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    // Perform an action (called when the card is dropped on a correct area)
    public void Action(Card card, Entity target)
    {
        this.card = card;
        this.target = target;

        switch (card.cardName)
        {
            case "Attack":
                AttackEnemy();
                break;
            case "Block":
                IncreaseBlock();
                break;
            case "Heal":
                HealPlayer();
                break;
            case "Strengthen":
                BuffPlayer(Buffs.Type.strength);
                break;
            case "Weaken":
                DebuffEnemy(Buffs.Type.weakness);
                break;
            case "Vulnerable":
                DebuffEnemy(Buffs.Type.vulnerable);
                break;
            default:
                Debug.Log("invalid card");
                break;
        }
    }

    // Deal damage to the enemy
    private void AttackEnemy()
    {
        if (target == null)
        {
            Debug.Log("Not dropped on enemy");
            return;
        }
        // Take weakness, vulnerable and strength into account
        int damageToDeal = card.effectAmount + user.strength.buffAmount - user.weakness.buffAmount;
        if (target.vulnerable.buffAmount > 0)
        {
            float boostedDamage = damageToDeal * 1.5f;
            damageToDeal = (int)boostedDamage;
        }
        target.Damaged(damageToDeal);
    }

    // Add block to the player
    private void IncreaseBlock()
    {
        user.AddBlock(card.effectAmount);
    }

    // Heal the player
    private void HealPlayer()
    {
        user.Heal(card.effectAmount);
    }

    // Buff the player
    private void BuffPlayer(Buffs.Type type)
    {
        user.BuffSelf(type, card.buffAmount);
    }

    // Debuff the enemy
    private void DebuffEnemy(Buffs.Type type)
    {
        target.BuffSelf(type, card.buffAmount);
        target.enemy.updateIntentOnDebuff();
    }
}