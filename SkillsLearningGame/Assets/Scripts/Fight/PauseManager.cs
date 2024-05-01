using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // To pause on escape key, maybe for future functionality
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or resume game accordingly
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

}
