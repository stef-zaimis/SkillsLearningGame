using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField]
    private LastResetStatControl statControlScript;

    // Sets all of the LastValues to the current values
    // When the SinceLastReset values are calculated they StatisticsValues are subtracted from the LastReset
    // values to give the value of the statistics since they were last reset.
    private static void UpdateLastValues()
    {
        StatisticValues.LastCorrectAnswers = StatisticValues.CorrectAnswers;
        StatisticValues.LastIncorrectAnswers = StatisticValues.IncorrectAnswers;
        StatisticValues.LastTotalDifficulty = StatisticValues.TotalDifficulty;
        StatisticValues.LastCorrectSubjects = StatisticValues.CorrectSubjects;
        StatisticValues.LastIncorrectSubjects = StatisticValues.IncorrectSubjects;
        StatisticValues.LastCardsUsed = StatisticValues.CardsUsed;
        StatisticValues.LastDamageDealt = StatisticValues.DamageDealt;
        StatisticValues.LastDamageTaken = StatisticValues.DamageTaken;
        StatisticValues.LastEnemiesDefeated = StatisticValues.EnemiesDefeated;
        StatisticValues.LastHealsGiven = StatisticValues.HealsGiven;
    }

    public void Reset()
    {
        UpdateLastValues();
        statControlScript.UpdateStatistics();
    }

    public static void ResetStatic()
    {
        UpdateLastValues();
    }
}