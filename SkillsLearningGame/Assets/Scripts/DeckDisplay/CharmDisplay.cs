using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmDisplay : MonoBehaviour
{

    [SerializeField]
    private GameObject charmPrefab;
    [SerializeField]
    private Transform parentTransform;
    private ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = FindObjectOfType<ScrollRect>();
        if (scrollRect != null)
        {
            ScrollToTop(scrollRect);
        }
        DisplayCharms();
    }

    // Iterates through each charm and adds it to the shop.
    public void DisplayCharms()
    {
        foreach (string charmName in CurrentCharms.OwnedCharms.Keys)
        {
            GameObject charmObject = Instantiate(charmPrefab, parentTransform);
            SetCharmInfo playerDeckCard = charmObject.GetComponent<SetCharmInfo>();
            if (charmName != null)
            {
                playerDeckCard.UpdateInformation(charmName);
            }
        }

        if (scrollRect != null)
        {
            ScrollToTop(scrollRect);
        }
    }

    // Changes the scroll to the top of the page.
    public void ScrollToTop(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
