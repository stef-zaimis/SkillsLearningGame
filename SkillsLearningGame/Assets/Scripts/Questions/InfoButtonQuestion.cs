using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoButtonQuestion : MonoBehaviour
{
    [SerializeField]
    private DifficultySetter difficultyScript;
    [SerializeField]
    private CategoryScript categoryScript;
    [SerializeField]
    private StarSizeAdjuster starAdjuster;
    [SerializeField]
    private TMP_Text difficultyText;
    [SerializeField]
    private TMP_Text categoryText;

    private QuestionControl questionControl;

    void Awake()
    {
        questionControl = FindObjectOfType<QuestionControl>();
    }

    public void onInfoButtonClicked()
    {
        difficultyScript.setDifficulty(questionControl.currQuestion.difficulty);
        categoryScript.setCategory(questionControl.currQuestion.category.ToString());
        //starAdjuster.UpdateStarsBasedOnDifficulty(questionControl.currQuestion.difficulty);
        setDifficultyText(questionControl.currQuestion.difficulty);
        setCategoryText(questionControl.currQuestion.category.ToString());
    }

    public void setDifficultyText(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                difficultyText.text = "Very Easy";
                break;
            case 2:
                difficultyText.text = "Easy";
                break;
            case 3:
                difficultyText.text = "Medium";
                break;
            case 4:
                difficultyText.text = "Hard";
                break;
            case 5:
                difficultyText.text = "Very Hard";
                break;
            default:
                Debug.Log("Invalid question difficulty");
                break;
        }
    }

    public void setCategoryText(string category)
    {
        switch (category)
        {
            case "AI":
                categoryText.text = "Artificial Intelligence Question";
                break;
            case "TI":
                categoryText.text = "Threat Intelligence Question";
                break;
            case "Cloud":
                categoryText.text = "Cloud Question";
                break;
            case "DS":
                categoryText.text = "Data Science Question";
                break;
            default:
                categoryText.text = "Invalid question category";
                break;
        }
    }
}