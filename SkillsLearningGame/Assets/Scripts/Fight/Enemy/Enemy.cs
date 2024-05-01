using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public List<EnemyActions> enemyActions;
    public List<EnemyActions> randomlyGeneratedEnemyTurns = new List<EnemyActions>();
    public int currentTurn;
    public bool ActionsToBeShuffled;
    public Entity currentEnemy;

    // Enemy intent is the action they will perform next turn, displayed to help the player set up a strategy
    [Header("Intent")]
    public TMP_Text intentText;
    public Image intentImage;
    public GameObject intentParent;

    BattleManager battleManager;
    Entity player;
    public bool midTurn;

    // Upon being awoken, get necessary game objects and shuffle actions, if they are to be shuffled
    void Awake()
    {
        LoadEnemy();
    }

    // Get necessary game objects
    public void LoadEnemy()
    {
        battleManager = FindObjectOfType<BattleManager>();
        player = battleManager.player;
        currentEnemy = GetComponent<Entity>();

        // If we want random actions, shuffle your actions
        if (ActionsToBeShuffled)
        {
            GenerateTurns();
        }
    }

    // Shuffle your actions
    public void GenerateTurns()
    {
        for (int i = 0; i < enemyActions.Count; i++)
        {
            for (int j = 0; j < enemyActions[i].chance; j++)
            {
                randomlyGeneratedEnemyTurns.Add(enemyActions[i]);
            }
        }
        randomlyGeneratedEnemyTurns.Shuffle();
    }

    // Attack the player
    private IEnumerator Attack()
    {
        // Take vulnerable and weakness into account
        int damageToDeal = randomlyGeneratedEnemyTurns[currentTurn].effectAmount + currentEnemy.strength.buffAmount - currentEnemy.weakness.buffAmount;
        yield return new WaitForSeconds(0.5f);
        if (player.vulnerable.buffAmount > 0)
        {
            float boostedDamage = damageToDeal * 1.5f;
            damageToDeal = (int)boostedDamage;
        }
        player.Damaged(damageToDeal);
        yield return new WaitForSeconds(0.5f);
        EndTurn();
    }

    // End your turn, i.e. change your action to display the next one, update your buffs and indicate you are done with your turn
    private void EndTurn()
    {
        currentTurn++;
        if (currentTurn >= randomlyGeneratedEnemyTurns.Count)
        {
            currentTurn = 0;
        }

        currentEnemy.UpdateBuffs();
        midTurn = false;
    }

    // Play your turn
    public void PlayTurn()
    {
        // Switch statement containing each action to be performed based on the current enemy turn action
        switch (randomlyGeneratedEnemyTurns[currentTurn].actionType)
        {
            case EnemyActions.ActionType.Attack:
                StartCoroutine(Attack());
                break;
            case EnemyActions.ActionType.Block:
                EnemyBlock();
                StartCoroutine(ApplyBuff());
                break;
            case EnemyActions.ActionType.Strengthen:
                BuffEnemy(Buffs.Type.strength);
                StartCoroutine(ApplyBuff());
                break;
            case EnemyActions.ActionType.Weaken:
                DebuffPlayer(Buffs.Type.weakness);
                StartCoroutine(ApplyBuff());
                break;
            case EnemyActions.ActionType.Vulnerable:
                DebuffPlayer(Buffs.Type.vulnerable);
                StartCoroutine(ApplyBuff());
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
    }

    // Placeholder function
    private IEnumerator ApplyBuff()
    {
        yield return new WaitForSeconds(1f);
        EndTurn();
    }

    // Add block to yourself
    private void EnemyBlock()
    {
        currentEnemy.AddBlock(randomlyGeneratedEnemyTurns[currentTurn].effectAmount);
    }

    // Display your next action
    public void DisplayIntent()
    {
        intentImage.sprite = randomlyGeneratedEnemyTurns[currentTurn].icon;

        //add strength to attack intent and remove weakness
        if (randomlyGeneratedEnemyTurns[currentTurn].actionType == EnemyActions.ActionType.Attack)
        {
            displayAttackIntent();
        }
        else
        {
            intentText.text = randomlyGeneratedEnemyTurns[currentTurn].effectAmount.ToString();
        }

        intentParent.SetActive(true);
    }

    // Hide your intent
    public void HideIntent()
    {
        intentParent.SetActive(false);
    }

    // Add a buff to yourself
    private void BuffEnemy(Buffs.Type type)
    {
        currentEnemy.BuffSelf(type, randomlyGeneratedEnemyTurns[currentTurn].effectAmount);
    }

    // Add a debuff to the player
    private void DebuffPlayer(Buffs.Type type)
    {
        player.BuffSelf(type, randomlyGeneratedEnemyTurns[currentTurn].effectAmount);
    }

    // Update your intent when the player debuffs you, so your attack values are changed dynamically during gameplay
    public void updateIntentOnDebuff()
    {
        if (randomlyGeneratedEnemyTurns[currentTurn].actionType == EnemyActions.ActionType.Attack)
        {
            displayAttackIntent();
        }
    }

    // Display the correct amount of damage you will deal
    public void displayAttackIntent()
    {
        int damageToDeal = randomlyGeneratedEnemyTurns[currentTurn].effectAmount + currentEnemy.strength.buffAmount - currentEnemy.weakness.buffAmount;
        if (player.vulnerable.buffAmount > 0)
        {
            float boostedDamage = damageToDeal * 1.5f;
            damageToDeal = (int)boostedDamage;
        }
        intentText.text = damageToDeal.ToString();
    }
}