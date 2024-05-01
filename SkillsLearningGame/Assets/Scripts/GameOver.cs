using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.SearchService; This is only used in the unity editor, but we did not use any functionality, so I am commenting it out

// Game over scene
public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelAmount;
    [SerializeField]
    private GameObject statisticsPrefab;
    [SerializeField]
    private GameObject instructionsPrefab;
    [SerializeField]
    private string toLinkTo = "https://skillsbuild.org";
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        if (levelAmount != null)
        {
            levelAmount.text = "Levels Passed: " + (GameManager.currentLevel - 1).ToString();
        }
    }

    public void StatisticsButton()
    {
        statisticsPrefab.SetActive(true);
    }

    public void InstructionsButton()
    {
        instructionsPrefab.SetActive(true);
    }

    public void LinkButton()
    {
        Application.OpenURL(toLinkTo);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        gameManager.ResetAllVariables();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
