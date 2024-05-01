using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySetter : MonoBehaviour
{

    [SerializeField]
    private GameObject difficultyItem;

    [SerializeField]
    private GameObject difficultyParent;

    [SerializeField]
    private bool respaceStars;

    public void setDifficulty(int difficulty)
    {
        // Destroy all but one star before instantiating new ones
        for (int i = difficultyParent.transform.childCount - 1; i > 0; i--)
        {
            Destroy(difficultyParent.transform.GetChild(i).gameObject);
        }

        // Adjust spacing for specifically 2 stars cause they are a bit finnicky
        if (difficulty == 2 && respaceStars)
        {
            HorizontalLayoutGroup difficultyParentLayoutGroup = difficultyParent.GetComponent<HorizontalLayoutGroup>();
            difficultyParentLayoutGroup.spacing = -105f;
        }

        // Instantiate stars
        for (int i = 0; i < difficulty - 1; i++)
        {
            Instantiate(difficultyItem, gameObject.transform);
        }
    }
}