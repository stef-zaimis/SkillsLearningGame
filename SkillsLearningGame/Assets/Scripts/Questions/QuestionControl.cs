using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class QuestionControl : MonoBehaviour
{
    [SerializeField]
    private List<Questions> questions;
    public Questions currQuestion;
    [SerializeField]
    private int correctAnswer;
    public static string correctAnswerText;

    [SerializeField]
    private GameObject readyScreen;

    [SerializeField]
    private TextMeshProUGUI questionContent;
    [SerializeField]
    private TextMeshProUGUI categoryContent;
    [SerializeField]
    private TextMeshProUGUI difficultyContent;
    [SerializeField]
    private AnswerControl[] answerButtons;
    [SerializeField]
    private LinkScript linkScript;
    [SerializeField]
    private DifficultySetter difficultyScript;
    [SerializeField]
    private CategoryScript categoryScript;


    private void Awake()
    {
        GetAllQuestions();
    }


    void Start()
    {
        if (readyScreen != null)
        {
            readyScreen.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        GetRandomQuestion();
        SetQuestionUI();
        SetAnswerUI();
    }

    private void GetRandomQuestion()
    {
        questions.Shuffle();
        bool questionAdded = false;
        int questionID = 0;
        while (!questionAdded && questionID < questions.Count)
        {
            Questions tempQuestion = questions[questionID];
            if (tempQuestion.cardSubject == GameManager.cardToAddSubject)
            {
                currQuestion = tempQuestion;
                correctAnswerText = "The correct answer was: " + "<color=yellow>" + currQuestion.answers[correctAnswer] + "</color>";
                questionAdded = true;
                return;
            }
            questionID++;
        }
        Debug.Log("No questions of that subject exist");
    }

    // Generates a list of questions from all of the questions within the Resources.Questions folder
    private void GetAllQuestions()
    {
        questions = new List<Questions>(Resources.LoadAll<Questions>("Questions"));
    }

    // Sets the information on the UI to the current text
    private void SetQuestionUI()
    {
        questionContent.text = currQuestion.question;
        categoryContent.text = currQuestion.category;
        linkScript.SetLink(currQuestion.link);
        difficultyScript.setDifficulty(currQuestion.difficulty);
        StatisticValues.TotalDifficulty += currQuestion.difficulty;
        categoryScript.setCategory(currQuestion.category.ToString());
    }

    // Sets the answers to randomized answers using randomAnswers()
    private void SetAnswerUI()
    {
        List<string> answers = RandomAnswers(new List<string>(currQuestion.answers));
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].SetAnswerContent(answers[i]);
            answerButtons[i].SetSubject(currQuestion.category.ToString());
            if (i == correctAnswer)
            {
                answerButtons[i].SetCorrect(true);
            }
            else
            {
                answerButtons[i].SetCorrect(false);
            }
        }
    }

    // Randomizes the list of answers
    private List<string> RandomAnswers(List<string> answers)
    {
        List<string> newAnswers = new List<string> { "", "", "", "" };
        List<int> availablePositions = new List<int> { 0, 1, 2, 3 };
        for (int pos = 0; pos < answerButtons.Length; pos++)
        {
            int chosenPos = Random.Range(0, availablePositions.Count);
            newAnswers[availablePositions[chosenPos]] = answers[pos];
            availablePositions.RemoveAt(chosenPos);
            if (pos == 0)
            {
                correctAnswer = chosenPos;
            }
        }
        return newAnswers;
    }

    public void StartQuestion()
    {
        readyScreen.SetActive(false);
        NextQuestion();
    }
}
