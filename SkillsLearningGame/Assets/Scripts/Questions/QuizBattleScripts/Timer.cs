using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField]
    float timeLeft;
    [SerializeField]
    bool timerOn = false;
    [SerializeField]
    public TextMeshProUGUI timerText;
    private QuizBattleQuestionControl questionControl;
    private RestGameControl restGameControl;

    // Handle the first second so it doesnt insta update
    private bool firstSecond = true;
    private float timeSinceStart = 0f;

    void Start()
    {
        questionControl = FindObjectOfType<QuizBattleQuestionControl>();
        restGameControl = FindObjectOfType<RestGameControl>();
        SetTimer(timeLeft);
    }

    public void PauseTimer()
    {
        timerOn = false;
    }

    public void StartTimer()
    {
        timerOn = true;
    }

    public void SetTimer(float newTime)
    {
        firstSecond = true;
        PauseTimer();
        timeLeft = newTime;
        UpdateTimerText(timeLeft);
    }


    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (firstSecond)
            {
                // Wait for one second before starting the countdown
                timeSinceStart += Time.deltaTime;
                if (timeSinceStart >= 1f)
                {
                    firstSecond = false;
                    timeSinceStart = 0f;
                }
                else
                {
                    return; // Wait for the initial delay to complete
                }
            }

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimerText(timeLeft);
            }
            else
            {
                Debug.Log("Time up");
                timeLeft = 0;
                PauseTimer();
                if (questionControl != null)
                {
                    questionControl.TimeUp();
                }
                else if (restGameControl != null)
                {
                    restGameControl.TimeUp();
                }
                else
                {
                    Debug.Log("There is no action to be performed at time up");
                }
            }
        }
    }

    void UpdateTimerText(float newTime)
    {
        float inMinutes = Mathf.FloorToInt(newTime / 60);
        float inSeconds = Mathf.FloorToInt(newTime % 60);

        if (newTime >= 0)
        {
            timerText.text = string.Format("{0:00} : {1:00}", inMinutes, inSeconds);
        }
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }
}