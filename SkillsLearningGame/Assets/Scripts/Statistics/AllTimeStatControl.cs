using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AllTimeStatControl : MonoBehaviour
{
    [SerializeField]
    private UpdateStatScript correctQuestions;
    [SerializeField]
    private UpdateStatScript incorrectQuestions;
    [SerializeField]
    private UpdateStatScript strongestSubject;
    [SerializeField]
    private UpdateStatScript weakestSubject;
    [SerializeField]
    private UpdateStatScript averageDifficulty;
    [SerializeField]
    private UpdateStatScript subjectAI;
    [SerializeField]
    private UpdateStatScript subjectCloud;
    [SerializeField]
    private UpdateStatScript subjectDS;
    [SerializeField]
    private UpdateStatScript subjectTI;
    [SerializeField]
    private UpdateStatScript enemiesDefeated;
    [SerializeField]
    private UpdateStatScript cardsUsed;
    [SerializeField]
    private UpdateStatScript damageDealt;
    [SerializeField]
    private UpdateStatScript damageTaken;
    [SerializeField]
    private UpdateStatScript healsGiven;


    // Start is called before the first frame update
    void Start()
    {
        UpdateStatistics();
    }

    void OnEnable()
    {
        UpdateStatistics();
    }

    // Calls all relevant functions to update all the statistics within the page
    public void UpdateStatistics()
    {
        UpdateCorrectQuestions();
        UpdateIncorrectQuestions();
        UpdateStrongestSubject();
        UpdateWeakestSubject();
        UpdateAverageDifficulty();
        UpdateSubject("AI", subjectAI);
        UpdateSubject("Cloud", subjectCloud);
        UpdateSubject("TI", subjectTI);
        UpdateSubject("DS", subjectDS);
        UpdateBattleStats();
    }

    // Calculates total questions answered and calls the script in the releveant GameObject to update the
    // correct questions answered statistic
    private void UpdateCorrectQuestions()
    {
        int total = StatisticValues.IncorrectAnswers + StatisticValues.CorrectAnswers;
        int correct = StatisticValues.CorrectAnswers;
        correctQuestions.UpdatePercentageStat(correct, total);
    }

    // Calculates total questions answered and calls the script in the releveant GameObject to update the
    // incorrect questions answered statistic
    private void UpdateIncorrectQuestions()
    {
        int total = StatisticValues.IncorrectAnswers + StatisticValues.CorrectAnswers;
        int incorrect = StatisticValues.IncorrectAnswers;
        incorrectQuestions.UpdatePercentageStat(incorrect, total);
    }

    // Works out which subject has been answered correctly the most times and updates the strongest subject statistic
    private void UpdateStrongestSubject()
    {
        string strongest = "None";
        int value = -1;
        foreach (KeyValuePair<string, int> entry in StatisticValues.CorrectSubjects)
        {
            if (entry.Value >= value)
            {
                value = entry.Value;
                strongest = entry.Key;
            }
        }
        strongestSubject.UpdateSubjectStat(strongest);
    }

    // Works out which subject has been answered incorrectly the most times and updates the weakest subject statistic
    private void UpdateWeakestSubject()
    {
        string weakest = "None";
        int value = -1;
        foreach (KeyValuePair<string, int> entry in StatisticValues.CorrectSubjects)
        {
            if (entry.Value >= value)
            {
                value = entry.Value;
                weakest = entry.Key;
            }
        }
        weakestSubject.UpdateSubjectStat(weakest);
    }

    // Calculates the average difficulty of questions answered and then updates the average difficulty statistic
    private void UpdateAverageDifficulty()
    {
        int totalAnswered = StatisticValues.CorrectAnswers + StatisticValues.IncorrectAnswers;
        int totalDifficulty = StatisticValues.TotalDifficulty;
        int avgDifficulty = 0;
        if (totalAnswered != 0)
        {
            avgDifficulty = Mathf.RoundToInt(totalDifficulty / totalAnswered);
        }
        averageDifficulty.UpdateDifficultyStat(avgDifficulty);
    }

    // Updates the subject specific statistic for the inputted subject
    private void UpdateSubject(string subject, UpdateStatScript subjectScript)
    {
        int correct = StatisticValues.CorrectSubjects[subject];
        int incorrect = StatisticValues.IncorrectSubjects[subject];
        subjectScript.UpdateSubjectStat(correct, incorrect);
    }

    // Updates the battle specific statistics based on the values stored in StatisticsValues static variables
    private void UpdateBattleStats()
    {
        enemiesDefeated.UpdateBattleStat(StatisticValues.EnemiesDefeated);
        cardsUsed.UpdateBattleStat(StatisticValues.CardsUsed);
        damageDealt.UpdateBattleStat(StatisticValues.DamageDealt);
        damageTaken.UpdateBattleStat(StatisticValues.DamageTaken);
        healsGiven.UpdateBattleStat(StatisticValues.HealsGiven);
    }
}
