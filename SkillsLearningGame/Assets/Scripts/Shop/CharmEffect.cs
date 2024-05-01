using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmEffect : MonoBehaviour
{
    private GameManager gameManager;
    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Decides what charm has been used and changes the player buffs accordingly.
    public void UseCharm(string name)
    {
        CurrentCharms.OwnedCharms[name] += 1;
        if (name == "Max Health Charm")
        {
            CurrentCharms.CurrentBuff[name] += 2f;
            GameManager.PlayerMaxHP += 2;
            gameManager.DisplayHud();
        }
        else if (name == "Damage Charm")
        {
            CurrentCharms.CurrentBuff[name] += 0.2f;
        }
        else if (name == "Energy Charm")
        {
            CurrentCharms.CurrentBuff[name] += 1;
            GameManager.MaxEnergy = GameManager.MaxEnergy + 1;
        }
        else if (name == "Starter Shield Charm")
        {
            CurrentCharms.CurrentBuff[name] += 1;
        }
        else if (name == "Absorption Charm")
        {
            CurrentCharms.CurrentBuff[name] += 0.2f;
        }
        else if (name == "Regen Charm")
        {
            CurrentCharms.CurrentBuff[name] += 1;
        }
        else if (name == "Hand Size Charm")
        {
            CurrentCharms.CurrentBuff[name] += 1;
        }
    }
}
