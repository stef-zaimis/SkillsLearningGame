using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A static class used to store information about all the Charms a player owns.
public static class CurrentCharms
{
    // OwnedCharms stores the number of each charm the player owns.
    public static Dictionary<string, int> OwnedCharms { get; set; }
    // CurrentBuff stores the value of the total effect of each type of charm the player owns.
    public static Dictionary<string, float> CurrentBuff { get; set; }
    // CharmSign stores the type of effect each type of charm causes (whether it increases, etc).
    public static Dictionary<string, string> CharmSign { get; set; }

    static CurrentCharms()
    {
        OwnedCharms = new Dictionary<string, int>()
        {
            {"Absorption Charm", 0},
            {"Damage Charm", 0},
            {"Energy Charm", 0},
            {"Max Health Charm", 0},
            {"Starter Shield Charm", 0},
            {"Regen Charm", 0},
            {"Hand Size Charm", 0},
        };

        CurrentBuff = new Dictionary<string, float>()
        {
            {"Absorption Charm", 1},
            {"Damage Charm", 1},
            {"Energy Charm", 0},
            {"Max Health Charm", 0},
            {"Starter Shield Charm", 0},
            {"Regen Charm", 0},
            {"Hand Size Charm", 0},
        };

        CharmSign = new Dictionary<string, string>()
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
