using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxCharmsReached : MonoBehaviour
{
    [SerializeField]
    private GameObject popupGameObject;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text descriptionText;

    // Shows the corresponding popup and changes the text relating to the charm inputted.
    public void ActivatePopup(string charmName)
    {
        popupGameObject.SetActive(true);
        titleText.text = "Maximum amount of " + charmName + " reached";
        descriptionText.text = "You have reached the maximum amount of " + charmName + " that you can own. The charm has not been added to your inventory.";
    }
}
