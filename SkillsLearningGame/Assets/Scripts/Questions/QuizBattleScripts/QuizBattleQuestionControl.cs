using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class QuizBattleQuestionControl : MonoBehaviour
{
    [SerializeField]
    private List<Questions> questions;
    public Questions currQuestion;
    [SerializeField]
    private int correctAnswer;

    [SerializeField]
    private TextMeshProUGUI questionContent;
    [SerializeField]
    private TextMeshProUGUI categoryContent;
    [SerializeField]
    private TextMeshProUGUI difficultyContent;
    [SerializeField]
    private QuizBattleAnswerControl[] answerButtons;
    [SerializeField]
    private LinkScript linkScript;
    [SerializeField]
    private DifficultySetter difficultyScript;
    [SerializeField]
    private CategoryScript categoryScript;
    [SerializeField]
    private TextMeshProUGUI pointContent;
    private int points = 0;
    public int totalQuestionDifficulty = 0;
    public static string correctAnswerText;
    public static string correctAnswerTextPlain;
    private GameManager gameManager;
    private SceneManager sceneManager;

    [SerializeField]
    private GameObject answerScreen;

    [SerializeField]
    private GameObject quizOverScreen;

    // If player should filter
    [SerializeField]
    private GameObject filterScreen;

    // To enable exit button
    [SerializeField]
    private GameObject exitButton;

    //To properly display the exit, settings and instructions with spacing
    [SerializeField]
    private HorizontalLayoutGroup exitButtonLayoutGroup;

    // To filter selected subjects
    [SerializeField]
    public List<Card.CardSubjectEnum> selectedSubjects = new List<Card.CardSubjectEnum>();

    // To filter selected difficulty
    public int minDifficulty = 1;
    public int maxDifficulty = 5;

    // No questions with the selected filters screen
    [SerializeField]
    private GameObject filterErrorScreen;

    // Player quiz over screen
    [SerializeField]
    private GameObject playerQuizOverScreen;

    // If player should not filter
    [SerializeField]
    private GameObject readyScreen;
    [SerializeField]
    private TMP_Text infoText;
    [SerializeField]
    private TMP_Text passText;
    private int toPass;

    // Pass and fail screens
    [SerializeField]
    private GameObject passScreen;
    [SerializeField]
    private GameObject failScreen;

    // Exit warning screen
    [SerializeField]
    private GameObject exitWarningScreen;

    // To properly handle the timer
    public Timer timer;

    private void Start()
    {
        GetAllQuestions();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.DisplayHud();

        sceneManager = FindObjectOfType<SceneManager>();

        if (GameManager.letPlayerFilterQuestions)
        {
            filterScreen.SetActive(true);
            exitButton.SetActive(true);
            exitButtonLayoutGroup.spacing = -100;
        }
        else
        {
            readyScreen.SetActive(true);
            SetUpLevelQuiz();
        }
    }

    public void NextQuestion()
    {
        GetRandomQuestion();
        SetQuestionUI();
        SetAnswerUI();
    }

    public void IncrementPoints(int toAdd)
    {
        points = points + toAdd;
        GameManager.points += toAdd;
        gameManager.DisplayHud();
    }

    public void IncrementPoints()
    {
        IncrementPoints(currQuestion.difficulty);
    }

    public void updateScore()
    {
        int percentage = Mathf.RoundToInt((float)points / totalQuestionDifficulty * 100);
        pointContent.text = $"Score: {points} / {totalQuestionDifficulty} ({percentage}%)";
        if (GameManager.letPlayerFilterQuestions) return;
        if (percentage >= toPass && !GameManager.letPlayerFilterQuestions) // If the player is passing make it green
        {
            pointContent.color = ColorUtility.TryParseHtmlString("#ABEC6E", out var color) ? color : Color.green;
        }
        else // else red
        {
            pointContent.color = Color.red;
        }
    }

    private void GetRandomQuestion()
    {
        List<Questions> filteredQuestions;

        // If it is a level quiz set all questions
        if (!GameManager.letPlayerFilterQuestions)
        {
            filteredQuestions = questions;
        }
        else // Else filter based on player's selections
        {
            // Filter the questions based on selected subjects
            filteredQuestions = questions.FindAll(question => selectedSubjects.Contains(question.cardSubject));

            // Filter questions based on selected difficulty
            filteredQuestions = filteredQuestions.FindAll(question => question.difficulty >= minDifficulty && question.difficulty <= maxDifficulty);
        }

        // If there are no questions found for the selected filter tell the user to try again
        if (filteredQuestions.Count == 0)
        {
            Debug.Log("No questions found for the selected filters, please try again");
            filterErrorScreen.SetActive(true);
        }
        else // Else start the quiz
        {
            // Selecting a random question off of the filtered list
            filteredQuestions.Shuffle();
            int randomIndex = Random.Range(0, filteredQuestions.Count);
            currQuestion = filteredQuestions[randomIndex];
            correctAnswerText = "The correct answer was: " + "<color=yellow>" + currQuestion.answers[correctAnswer] + "</color>";
            correctAnswerTextPlain = currQuestion.answers[correctAnswer];
        }
    }

    public void HideFilterErrorScreen()
    {
        filterErrorScreen.SetActive(false);
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
            answerButtons[i].SetCorrect(i == correctAnswer);
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

    public void NavigateToQuiz()
    {
        NextQuestion();
        timer.StartTimer();
        filterScreen.SetActive(false);
        readyScreen.SetActive(false);
        if (!GameManager.letPlayerFilterQuestions)
        {
            pointContent.color = Color.red;
        }
    }

    // When exiting, check if he has gotten points
    public void ExitButtonPressed()
    {
        // If the quiz is over, or hasn't started just go back to map
        if (quizOverScreen.activeSelf || filterScreen.activeSelf)
        {
            BackToMapPressed();
            return;
        }

        // Else warn the user
        exitWarningScreen.SetActive(true);
        timer.PauseTimer();
    }

    // If the player exits, dont give him his points
    public void ExitAnywayButtonPressed()
    {
        GameManager.points -= points;
        if (!GameManager.letPlayerFilterQuestions)
        {
            GameManager.updateNodes = true;
        }
        else
        {
            GameManager.updateNodes = false;
        }
        BackToMapPressed();
    }

    // If the player chooses not to quit the quiz
    public void StayInQuizButtonPressed()
    {
        exitWarningScreen.SetActive(false);
        if (!answerScreen.activeSelf && !readyScreen.activeSelf && !filterScreen.activeSelf && !quizOverScreen.activeSelf)
        {
            timer.StartTimer();
        }
    }

    public void ContinueButtonPressed()
    {
        sceneManager.chooseScene("LevelSelectionScene");
    }

    public void RetakeQuizButtonPressed()
    {
        sceneManager.chooseScene("QuizBattle");
    }

    public void BackToMapPressed()
    {
        if (GameManager.updateNodes && GameManager.currentLevel > 0)
        {
            GameManager.currentLevel -= 1;
        }
        GameManager.letPlayerFilterQuestions = false;
        sceneManager.chooseScene("LevelSelectionScene");
    }

    public void TimeUp()
    {
        timer.SetTimer(0);
        quizOverScreen.SetActive(true);
        if (GameManager.letPlayerFilterQuestions) // If this is a player quiz (hence no pass or fail) load the plain quiz over screen and return
        {
            playerQuizOverScreen.SetActive(true);
            return;
        }
        // Else load the pass or fail screens respectively
        int percentage = Mathf.RoundToInt((float)points / totalQuestionDifficulty * 100);
        if (percentage >= toPass) // If the player passed
        {
            GameManager.updateNodes = false;
            passScreen.SetActive(true);
        }
        else // else red   
        {
            GameManager.updateNodes = true;
            failScreen.SetActive(true);
        }
    }

    // This function is meant to set up the basic attributes of the level quiz (timer, score to pass, question difficulty, subjects etc)
    public void SetUpLevelQuiz()
    {
        int levels = GameManager.currentLevel;
        int time = 10;
        if (levels >= 0 && levels <= 4)
        {
            timer.SetTimer(time);
            minDifficulty = 1;
            maxDifficulty = 5;
            toPass = 40;
        }
        else if (levels > 4 && levels < 8)
        {
            timer.SetTimer(time);
            minDifficulty = 1;
            maxDifficulty = 5;
            toPass = 40;
        }
        else if (levels >= 8 && levels < 10)
        {
            timer.SetTimer(time);
            minDifficulty = 1;
            maxDifficulty = 5;
            toPass = 40;
        }
        else if (levels >= 10)
        {
            timer.SetTimer(time);
            minDifficulty = 1;
            maxDifficulty = 5;
            toPass = 40;
        }
        SetReadyScreenUI(time);
    }

    // This sets the ready screen texts to display the proper variables
    private void SetReadyScreenUI(int time)
    {
        if (minDifficulty == maxDifficulty)
        {
            string difficultyText = SetDifficultyText(minDifficulty);
            infoText.text = "A " + time + " second quiz containing all subjects and of difficulty " + difficultyText + " will begin once you press start.";
        }
        else
        {
            string minText = SetDifficultyText(minDifficulty);
            string maxText = SetDifficultyText(maxDifficulty);
            infoText.text = "A " + time + " second quiz containing all subjects and difficulties ranging from " + minText + " to " + maxText + " will begin once you press start.";
        }
        passText.text = "To pass the quiz and move on to the next level you need to score " + toPass + "% and over.";
    }

    private string SetDifficultyText(int value)
    {
        switch (value)
        {
            case 1:
                return "Very Easy";
            case 2:
                return "Easy";
            case 3:
                return "Medium";
            case 4:
                return "Hard";
            case 5:
                return "Very Hard";
            default:
                Debug.Log("Out of bounds difficulty, returning Medium");
                return "Medium";
        }
    }
}