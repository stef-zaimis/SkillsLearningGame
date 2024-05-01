using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopupContinue : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPopup;

    // Shows popup on function call
    public void OnButtonPress()
    {
        healthPopup.SetActive(false);
    }
}