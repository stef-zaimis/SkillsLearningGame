using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionManager : MonoBehaviour
{
    public GameObject cardPanel;
    public GameObject charmPanel;
    public ScrollRect scrollRect;

    public SectionButton charmButton;
    public SectionButton cardButton;

    public ShopManager shopManager;

    void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void switchToCards()
    {
        charmPanel.SetActive(false);

        shopManager.inCharmSection = false;

        charmButton.inThisSection = false;
        cardButton.inThisSection = true;

        cardButton.updateColor();
        charmButton.updateColor();

        cardPanel.SetActive(true);

        shopManager.LoadShopCards();

        // Reset the scroll rect content
        scrollRect.content = cardPanel.GetComponent<RectTransform>();
        ResetScrollPosition();
    }

    public void switchToCharms()
    {
        cardPanel.SetActive(false);

        shopManager.inCharmSection = true;

        cardButton.inThisSection = false;
        charmButton.inThisSection = true;

        charmButton.updateColor();
        cardButton.updateColor();

        charmPanel.SetActive(true);

        // Reset the scroll rect content
        scrollRect.content = charmPanel.GetComponent<RectTransform>();

        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.horizontalNormalizedPosition = 0.0f;

    }
}
