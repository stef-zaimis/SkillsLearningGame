using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace
using TMPro;

public class TimeSliderController : MonoBehaviour
{
    public Slider timeSlider; // Reference to the slider
    public TMP_Text timeDisplay; // Reference to a UI Text to display the time
    private QuizBattleQuestionControl questionControl;

    void Start()
    {
        questionControl = FindObjectOfType<QuizBattleQuestionControl>();
        if (timeSlider != null)
        {
            // Add a listener to call the OnSliderChanged method whenever the slider's value changes
            timeSlider.onValueChanged.AddListener(OnSliderChanged);
            // Initialize the display
            OnSliderChanged(timeSlider.value);
        }
    }

    public void OnSliderChanged(float value)
    {
        // Each step on the slider represents 30 seconds. Calculate the total seconds.
        // The '+1' accounts for starting at 30 seconds instead of 0.
        int totalSeconds = ((int)value * 30) + 30;

        // Convert totalSeconds to minutes and seconds
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        // Update the display Text
        if (timeDisplay != null)
        {
            timeDisplay.text = "Selected Time: " + $"{minutes:00}:{seconds:00}";
        }
        questionControl.timer.SetTimer((float)totalSeconds);
    }
}