using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatisticValues
{
    public static int CorrectAnswers { get; set; }
    public static int IncorrectAnswers { get; set; }
    public static Dictionary<string, int> CorrectSubjects { get; set; }
    public static Dictionary<string, int> IncorrectSubjects { get; set; }
    public static int TotalDifficulty { get; set; }
    public static int LastCorrectAnswers { get; set; }
    public static int LastIncorrectAnswers { get; set; }
    public static Dictionary<string, int> LastCorrectSubjects { get; set; }
    public static Dictionary<string, int> LastIncorrectSubjects { get; set; }
    public static int LastTotalDifficulty { get; set; }
    public static int LastScene { get; set; }
    public static int EnemiesDefeated { get; set; }
    public static int CardsUsed { get; set; }
    public static int DamageDealt { get; set; }
    public static int DamageTaken { get; set; }
    public static int HealsGiven { get; set; }
    public static int LastEnemiesDefeated { get; set; }
    public static int LastCardsUsed { get; set; }
    public static int LastDamageDealt { get; set; }
    public static int LastDamageTaken { get; set; }
    public static int LastHealsGiven { get; set; }

    // Initialises the initial values, currently using placeholder values to test that other scripts work
    static StatisticValues()
    {
        LastScene = 0;
        CorrectAnswers = 0;
        IncorrectAnswers = 0;
        TotalDifficulty = 0;

        EnemiesDefeated = 0;
        CardsUsed = 0;
        DamageDealt = 0;
        DamageTaken = 0;
        HealsGiven = 0;

        LastEnemiesDefeated = 0;
        LastCardsUsed = 0;
        LastDamageDealt = 0;
        LastDamageTaken = 0;
        LastHealsGiven = 0;

        CorrectSubjects = new Dictionary<string, int>()
            {
                {"Cloud", 0},
                {"AI", 0},
                {"TI", 0},
                {"DS", 0}
            };
        IncorrectSubjects = new Dictionary<string, int>()
            {
                {"Cloud", 0},
                {"AI", 0},
                {"TI", 0},
                {"DS", 0}
            };

        LastCorrectAnswers = 0;
        LastIncorrectAnswers = 0;
        LastTotalDifficulty = 0;
        LastCorrectSubjects = new Dictionary<string, int>()
            {
                {"Cloud", 0},
                {"AI", 0},
                {"TI", 0},
                {"DS", 0}
            };
        LastIncorrectSubjects = new Dictionary<string, int>()
            {
                {"Cloud", 0},
                {"AI", 0},
                {"TI", 0},
                {"DS", 0}
            };
    }
}
