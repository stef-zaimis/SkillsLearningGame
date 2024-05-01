using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class HelpButton : MonoBehaviour
{
    private bool mouseOver = false;
    private float lastHideTime = 0f; // Track when the panel was last hidden
    private float cooldown = 0.5f; // Cooldown period in seconds

    [SerializeField]
    private GameObject helpText;

    public void Start()
    {
        helpText.SetActive(false);
    }

    public void OnClick()
    {
        helpText.SetActive(true);
    }

    private IEnumerator ShowHelpTextAfterDelay()
    {
        mouseOver = true;
        yield return new WaitForSeconds(0.2f);
        if (mouseOver && Time.time >= lastHideTime + cooldown)
        {
            helpText.SetActive(true);
        }
    }

    public void OnEnterHelpButton()
    {
        StartCoroutine(ShowHelpTextAfterDelay());
    }

    public void OnInfoButtonExit()
    {
        mouseOver = false;
    }

    public void OnInfoPanelExit()
    {
        mouseOver = false;
        if (helpText.activeSelf)
        {
            helpText.SetActive(false);
            lastHideTime = Time.time;
        }
    }
}