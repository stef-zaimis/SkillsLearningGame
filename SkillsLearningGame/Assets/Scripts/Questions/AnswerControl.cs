using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AnswerControl : MonoBehaviour
{
    [SerializeField]
    private bool correctAnswer;

    [SerializeField]
    private TextMeshProUGUI ansContent;

    [SerializeField]
    private QuestionControl questionControl;

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
        GameManager.answeredCorrectly = correctAnswer;
        gameManager.evaluateAddingCard();
        questionPanel.SetActive(false);
        correctAnswerText.text = QuestionControl.correctAnswerText;
        questionAnsweredScreen.SetActive(true);

        if (correctAnswer)
        {
            correctAnswerPanel.SetActive(true);
            StatisticValues.CorrectAnswers += 1;
            StatisticValues.CorrectSubjects[subject] += 1;

            Debug.Log("Correct answer pressed");
        }
        else
        {
            incorrectAnswerPanel.SetActive(true);
            StatisticValues.IncorrectAnswers += 1;
            StatisticValues.IncorrectSubjects[subject] += 1;
            Debug.Log("Incorrect answer pressed");
        }
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
    }
}