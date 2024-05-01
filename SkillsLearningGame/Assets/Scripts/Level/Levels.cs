using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This handles the loading of different levels in functions for the map to call
public class Levels : MonoBehaviour
{
    public static Level level;
    private static SceneManager sceneManager;
    private static GameManager gameManager;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public static void StartFightLevel()
    {
        level.levelType = Level.Type.fight;
        Debug.Log("In start fight level");
        FindObjectOfType<GameManager>().increaseLevel();
        FindObjectOfType<SceneManager>().chooseScene("Fight");
    }

    public static void StartBossLevel()
    {
        level.levelType = Level.Type.bossFight;
        Debug.Log("In boss fight");
        FindObjectOfType<GameManager>().increaseLevel();
        FindObjectOfType<SceneManager>().chooseScene("Fight");
    }

    public static void StartQuizBattleLevel()
    {
        Debug.Log("In start quiz battle level");
        GameManager.letPlayerFilterQuestions = false;
        FindObjectOfType<GameManager>().increaseLevel();
        FindObjectOfType<SceneManager>().chooseScene("QuizBattle");
    }

    public static void StartRestLevel()
    {
        Debug.Log("In rest level");
        FindObjectOfType<GameManager>().increaseLevel();
        FindObjectOfType<SceneManager>().chooseScene("RestScene");
    }
}