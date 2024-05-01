using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyFilters : MonoBehaviour
{
    [SerializeField] private DoubleSlider slider;
    [SerializeField] private TextMeshProUGUI difficultyRangeText;

    private QuizBattleQuestionControl questionControl;

    void Awake()
    {
        slider.OnValueChanged.AddListener(SliderValueChanged);
        questionControl = FindObjectOfType<QuizBattleQuestionControl>();
    }

    private void SliderValueChanged(float min, float max)
    {
        questionControl.minDifficulty = Mathf.RoundToInt(min);
        questionControl.maxDifficulty = Mathf.RoundToInt(max);
        string minText = SetSliderText(Mathf.RoundToInt(min));
        string maxText = SetSliderText(Mathf.RoundToInt(max));
        if (minText == maxText)
        {
            difficultyRangeText.text = minText;
        }
        else
        {
            difficultyRangeText.text = string.Format("{0} - {1}", minText, maxText);
        }
    }

    private string SetSliderText(int value)
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
