using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

// This script manages the scene transitions
public class SceneManager : MonoBehaviour
{

    public GameObject mainMenuScene;
    public GameObject battleScene;
    public GameObject levelScreen;

    public GameObject playerIcon;

    GameManager gameManager;
    BattleManager battleManager;

    WinScreen winScreen;

    public enum Level { fight, bossFight };

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
        winScreen = FindObjectOfType<WinScreen>();
    }

    // Load the main menu when the game starts
    private void Start()
    {
        LoadScene("MainMenu");
    }

    // Navigate to level selection screen
    public void StartButton()
    {
        Debug.Log("Button Pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // Navigate to the instruction screen
    public void InstructionButton()
    {
        Debug.Log("Instruction Button Pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    // Quit the game
    public void QuitButton()
    {
        Application.Quit();
    }

    public void LeaveStatsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(StatisticValues.LastScene);
    }

    // Load a fight scene of either boss fight or normal fight
    public void StartBattle(string type)
    {
        StartCoroutine(LoadBattle(type));
    }

    // Load given scene
    public void chooseScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    // When loading fight scene
    public IEnumerator LoadBattle(string type)
    {
        //fade out
        //Cursor.lockState = CursorLockMode.Locked;
        //StartCoroutine(sceneFader.UI_Fade());
        yield return new WaitForSeconds(1);
        Debug.Log("we are in fight");

        playerIcon.SetActive(true);

        if (type == "fight")
        {
            battleManager.BeginFight();
        }
        else if (type == "bossFight")
        {
            battleManager.BeginBossFight();
        }

        //fade in
        yield return new WaitForSeconds(1);
        //Cursor.lockState = CursorLockMode.None;
    }

    // When load scene is called
    public IEnumerator LoadScene(string scene)
    {
        Debug.Log(scene);
        //fade out
        //StartCoroutine(sceneFader.UI_Fade());
        //yield return new WaitForSeconds(1);
        //playerIcon.SetActive(true);
        if (scene != "SettingsMenu")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }

        if (scene == "Fight")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
        else if (scene == "MainMenu")
        {
            playerIcon.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (scene == "LevelSelection" || scene == "LevelSelectionScene")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else if (scene == "GameOver")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else if (scene == "QuestionScene")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
        else if (scene == "QuizBattle")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        }
        else if (scene == "Shop")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(7);
        }
        else if (scene == "StatisticsScene")
        {
            StatisticValues.LastScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(9);
        }
        else if (scene == "RestScene" || scene == "Rest")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(10);
        }

        //fade in
        yield return new WaitForSeconds(1);
    }

    public void LoadSettingsMenu()
    {
        Time.timeScale = 0; // Pause current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(8, LoadSceneMode.Additive);
    }

    public void CloseSettingsMenu()
    {
        // Unpause the game
        Time.timeScale = 1;
        // Unload the scene
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(8);
    }
}
