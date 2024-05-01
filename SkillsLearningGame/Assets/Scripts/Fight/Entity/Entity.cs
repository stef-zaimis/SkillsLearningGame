using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int initialMaxHP;
    public int maxHP;
    public int currentHP;
    public int block;

    [Header("Buffs")]
    public Buffs strength;
    public Buffs weakness;
    public Buffs vulnerable;
    public Transform buffParent;
    public GameObject buffObject;

    public bool player;
    public EntityHPBar entityHPBar;
    public Enemy enemy;
    [SerializeField]
    private GameObject dmgIndPrefab;
    [SerializeField]
    private GameObject fallingNotificationPrefab;
    BattleManager battleManager;
    GameManager gameManager;

    // On awake, get necessary game objects and behave accordingly based on if this entity is the player or not
    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();

        // If this is the player, set your HP accordingly
        if (player)
        {
            if (GameManager.PlayerMaxHP == 0)
            {
                GameManager.PlayerMaxHP = initialMaxHP;
                GameManager.PlayerCurrentHP = initialMaxHP;
            }
            maxHP = GameManager.PlayerMaxHP;
            currentHP = GameManager.PlayerCurrentHP + (int)CurrentCharms.CurrentBuff["Regen Charm"];
            currentHP = Mathf.Min(currentHP, maxHP);
        }
        // If this is the enemy set your HP accordingly
        else
        {
            maxHP = initialMaxHP;
            currentHP = maxHP;
        }

        // Display your HP and HP Bar accordingly
        entityHPBar.hpBarSlider.maxValue = maxHP;
        entityHPBar.ShowHP(currentHP);

        if (player)
        {
            gameManager.DisplayHP(currentHP, maxHP);
        }
    }

    // Upon getting hit
    public void Damaged(int damageAmount)
    {
        // Take block into account
        if (!player)
        {
            damageAmount = Mathf.RoundToInt(damageAmount * CurrentCharms.CurrentBuff["Damage Charm"]);
        }
        else
        {
            damageAmount = Mathf.RoundToInt(damageAmount / CurrentCharms.CurrentBuff["Absorption Charm"]);
        }
        if (block > 0)
        {
            damageAmount = Block(damageAmount);
        }
        CreateDamageIndicator(damageAmount.ToString(), -1);
        currentHP -= damageAmount;
        if (player)
        {
            StatisticValues.DamageTaken += damageAmount;
        }
        else
        {
            StatisticValues.DamageDealt += damageAmount;
        }
        updateHPBar(currentHP);

        // If you die, end the fight
        if (player)
        {
            GameManager.PlayerCurrentHP = currentHP;
        }
        if (currentHP <= 0)
        {
            if (!player)
            {
                StatisticValues.EnemiesDefeated += 1;
            }
            if (enemy != null)
            {
                battleManager.FightEnd(true);
            }
            else
            {
                battleManager.FightEnd(false);
            }
            Destroy(gameObject);
        }
    }

    // Adding block
    public void AddBlock(int blockAmount)
    {
        block += blockAmount;
        entityHPBar.ShowBlock(block);
    }

    // Performing a block action upon getting hit
    public int Block(int damageAmount)
    {
        if (damageAmount > block)
        {
            damageAmount -= block;
            block = 0;
        }
        else
        {
            block -= damageAmount;
            damageAmount = 0;
        }
        entityHPBar.ShowBlock(block);

        return damageAmount;
    }

    // Heal yourself
    public void Heal(int healAmount)
    {
        if (currentHP + healAmount <= maxHP)
        {
            currentHP += healAmount;
        }
        else if (currentHP != maxHP)
        {
            currentHP = maxHP;
        }
        int hpIncrease = Mathf.Min(maxHP - currentHP, healAmount);
        if (player)
        {
            StatisticValues.HealsGiven += hpIncrease;
        }
        CreateDamageIndicator(hpIncrease.ToString(), 1);
        updateHPBar(currentHP);
        if (player)
        {
            GameManager.PlayerCurrentHP = currentHP;
        }
    }

    // Set game object to inactive if killed
    private void Killed()
    {
        this.gameObject.SetActive(false);
    }

    // Reset your block
    public void Reset()
    {
        block = 0;
        entityHPBar.ShowBlock(0);
    }

    // Update your healthbar
    public void updateHPBar(int updateHP)
    {
        currentHP = updateHP;
        entityHPBar.ShowHP(updateHP);
        if (player)
        {
            gameManager.DisplayHP(updateHP, maxHP);
        }
    }

    // Set the initial player HP
    private void initialisePlayerHP()
    {
        maxHP = initialMaxHP;
        if (player)
        {
            GameManager.PlayerMaxHP = maxHP;
            GameManager.PlayerCurrentHP = maxHP; // Reset player's current HP if needed
        }
    }

    // Apply a buff/debuff to yourself (debuffs are applied by enemy actions, but your own buffself is called to apply them)
    public void BuffSelf(Buffs.Type buffType, int buffAmount)
    {
        // If strength buff
        if (buffType == Buffs.Type.strength)
        {
            if (strength.buffAmount <= 0)
            {
                strength.buffUI = Instantiate(buffObject, buffParent).GetComponent<BuffUI>();
            }
            strength.buffAmount += buffAmount;
            strength.buffUI.DisplayBuff(strength);
        }
        // If weakness debuff
        else if (buffType == Buffs.Type.weakness)
        {
            if (weakness.buffAmount <= 0)
            {
                weakness.buffUI = Instantiate(buffObject, buffParent).GetComponent<BuffUI>();
            }
            weakness.buffAmount += buffAmount;
            weakness.buffUI.DisplayBuff(weakness);
        }
        // If vulnerable debuff
        else if (buffType == Buffs.Type.vulnerable)
        {
            if (vulnerable.buffAmount <= 0)
            {
                vulnerable.buffUI = Instantiate(buffObject, buffParent).GetComponent<BuffUI>();
            }
            vulnerable.buffAmount += buffAmount;
            vulnerable.buffUI.DisplayBuff(vulnerable);
        }
    }

    // Reset your buffs and debuffs
    public void ResetBuffs()
    {
        if (strength.buffAmount > 0)
        {
            strength.buffAmount = 0;
            Destroy(strength.buffUI.gameObject);
        }

        else if (weakness.buffAmount > 0)
        {
            weakness.buffAmount = 0;
            Destroy(weakness.buffUI.gameObject);
        }

        else if (vulnerable.buffAmount > 0)
        {
            vulnerable.buffAmount = 0;
            Destroy(vulnerable.buffUI.gameObject);
        }
    }

    // Update your buffs and debuffs (called at the end of a turn)
    public void UpdateBuffs()
    {
        if (vulnerable.buffAmount > 0)
        {
            vulnerable.buffAmount -= 1;
            vulnerable.buffUI.DisplayBuff(vulnerable);

            if (vulnerable.buffAmount <= 0)
                Destroy(vulnerable.buffUI.gameObject);
        }
    }

    public void CreateDamageIndicator(string newText, int sign)
    {
        Vector3 pos = transform.position;
        var obj = Instantiate(dmgIndPrefab, pos, Quaternion.identity);
        obj.transform.parent = transform.parent;
        var message = obj.GetComponent<DamageIndicators>();
        message.SetMessage(newText, sign);
        float xVel = Random.Range(30, 100) * (Random.Range(0, 2) * 2 - 1);
        float yVel = Random.Range(10, 30);
        Rigidbody2D rigidBody = obj.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(xVel, yVel);
        pos.y += Random.Range(-50, 200);
        if (xVel < 0)
        {
            pos.x += Random.Range(50, 100);
        }
        else
        {
            pos.x -= Random.Range(50, 100);
        }
        obj.transform.position = pos;
    }

    private void HealthChange(string sign, int amount)
    {
        Vector2 pos = new Vector2(190, 455);
        var obj = Instantiate(fallingNotificationPrefab, pos, Quaternion.identity);
        obj.transform.SetParent(transform.parent);
        var message = obj.GetComponent<FallingNotification>();
        if (sign == "+")
        {
            message.SetText(sign + amount.ToString() + " hp", Color.green);
        }
        else if (sign == "-")
        {
            message.SetText(sign + amount.ToString() + " hp", Color.red);
        }
        else
        {
            message.SetText(sign + amount.ToString() + " hp", Color.white);
        }
    }
}