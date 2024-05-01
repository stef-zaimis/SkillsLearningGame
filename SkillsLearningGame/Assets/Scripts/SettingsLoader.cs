using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsLoader : MonoBehaviour
{
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
