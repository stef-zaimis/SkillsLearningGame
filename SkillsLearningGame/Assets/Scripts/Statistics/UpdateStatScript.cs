using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateStatScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainText;
    [SerializeField]
    private TextMeshProUGUI hoverText;
    [SerializeField]
    private GameObject[] images;
    [SerializeField]
    private GameObject difficultyItem;
    [SerializeField]
    private TextMeshProUGUI subjectTotal;
    [SerializeField]
    private TextMeshProUGUI subjectCorrect;
    [SerializeField]
    private TextMeshProUGUI subjectCorrectHover;
    [SerializeField]
    private TextMeshProUGUI subjectIncorrect;
    [SerializeField]
    private TextMeshProUGUI subjectIncorrectHover;

    // Sets the mainText to the inputted value and calculated the percentage, and sets the hoverText to said value 
    // Used to update Correct Questions Answered and Incorrect Questions Answered statistics
    public void UpdatePercentageStat(int value, int totalValue)
    {
        mainText.text = value.ToString();
        int percentage = 0;
        if (totalValue != 0)
        {
            percentage = Mathf.RoundToInt(value * 100 / totalValue);
        }
        hoverText.text = percentage.ToString() + "%";
    }

    // Changes the text of the relevant subject stat, and then calls UpdateImage() to set the associated image to active
    // Used to update strongest subject and weakest subject statistics
    public void UpdateSubjectStat(string subject)
    {
        if (subject == "DS")
        {
            mainText.text = "Data Science";
        }
        else if (subject == "AI")
        {
            mainText.text = "Artificial Intelligence";
        }
        else if (subject == "TI")
        {
            mainText.text = "Threat Intelligence";
        }
        else
        {
            mainText.text = subject;
        }
        UpdateImage(subject);
    }

    // Searches for the image associated with the inputted subject and sets it to active, setting all other images to inactive
    // to prevent images overlaying one another
    private void UpdateImage(string category)
    {
        foreach (GameObject image in images)
        {
            if (image.name == category)
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }
        }
    }

    // Changes the text of the difficulty stat and then calls UpdateDifficultyImage() to update image part of the statistic.
    // Used to update average difficulty statistic
    public void UpdateDifficultyStat(int value)
    {
        mainText.text = value.ToString();
        UpdateDifficultyImage(value);
    }

    // Calls RemoveExcessDifficultyImages to ensure only 1 difficulty icon is show, then duplicates the image difficulty-1 times
    private void UpdateDifficultyImage(int difficulty)
    {
        RemoveExcessDifficultyImages();
        for (int i = 0; i < difficulty - 1; i++)
        {
            Instantiate(difficultyItem, difficultyItem.transform.parent);
        }
    }

    // Iterates through difficultyItem's siblings checking for any duplicates of the difficulty icon and destroying them
    private void RemoveExcessDifficultyImages()
    {
        Transform parent = difficultyItem.transform.parent;
        foreach (Transform child in parent)
        {
            GameObject childGameObject = child.GameObject();
            if (childGameObject != difficultyItem)
            {
                Destroy(childGameObject);
            }
        }
    }

    // Sets all of the Subject Specific Statistics for the corresponding subject using inputted values
    // Used to update all the statistics in Subject Specific Statistics Category
    public void UpdateSubjectStat(int correct, int incorrect)
    {
        int total = correct + incorrect;
        int correctPercentage = 100;
        if (total != 0)
        {
            correctPercentage = Mathf.RoundToInt(correct * 100 / total);
        }
        int incorrectPercentage = 100 - correctPercentage;

        subjectTotal.text = total.ToString();
        subjectCorrect.text = correct.ToString();
        subjectIncorrect.text = incorrect.ToString();
        subjectCorrectHover.text = correctPercentage.ToString() + "%";
        subjectIncorrectHover.text = incorrectPercentage.ToString() + "%";
    }

    // Updates the text in the battle set to the corresponding inputted value.
    public void UpdateBattleStat(int value)
    {
        mainText.text = value.ToString();
    }

}
