using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuizBattleAnswerControl : MonoBehaviour
{
    [SerializeField]
    private bool correctAnswer;

    [SerializeField]
    private TextMeshProUGUI ansContent;

    [SerializeField]
    private QuizBattleQuestionControl questionControl;

    private GameManager gameManager;

    public GameObject questionAnsweredScreen;
    public GameObject correctAnswerPanel;
    public GameObject incorrectAnswerPanel;
    public GameObject questionPanel;
    public TMP_Text correctAnswerText;
    private String subject;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Setter method for whether the answer is correct or not
    public void SetCorrect(bool correct)
    {
        correctAnswer = correct;
    }

    // Setter method for the answer text
    public void SetAnswerContent(string content)
    {
        ansContent.text = content;
    }

    public void SetSubject(string newSubject)
    {
        subject = newSubject;
    }

    // Function to occur when answer is clicked
    public void OnClick()
    {
        bool isCorrect = ansContent.text == QuizBattleQuestionControl.correctAnswerTextPlain;

        GameManager.answeredCorrectly = isCorrect;
        //questionPanel.SetActive(false);
        correctAnswerText.text = QuizBattleQuestionControl.correctAnswerText;
        questionAnsweredScreen.SetActive(true);
        questionControl.timer.PauseTimer();

        questionControl.totalQuestionDifficulty += questionControl.currQuestion.difficulty;

        if (isCorrect)
        {
            StartCoroutine(ShowCorrectAnswerPanelBriefly());
            StatisticValues.CorrectAnswers += 1;
            StatisticValues.CorrectSubjects[subject] += 1;
            questionControl.IncrementPoints();
            Debug.Log("Correct answer pressed");
        }
        else
        {
            incorrectAnswerPanel.SetActive(true);
            StatisticValues.IncorrectAnswers += 1;
            StatisticValues.IncorrectSubjects[subject] += 1;
            Debug.Log("Incorrect answer pressed");
        }
        questionControl.updateScore();
    }

    IEnumerator ShowCorrectAnswerPanelBriefly()
    {
        correctAnswerPanel.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        correctAnswerPanel.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        NextQuestion();
    }

    public void LoadLevelSelectionScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void NextQuestion()
    {
        correctAnswerPanel.SetActive(false);
        incorrectAnswerPanel.SetActive(false);
        questionAnsweredScreen.SetActive(false);
        questionPanel.SetActive(true);
        questionControl.NextQuestion();
        questionControl.timer.StartTimer();
    }
}