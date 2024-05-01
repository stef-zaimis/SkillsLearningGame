using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateCharmStats : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currentlyOwned;
    [SerializeField]
    private TMP_Text currentBuff;


    // Start is called before the first frame update
    void Start()
    {
        UpdateValues();
    }

    // Gets the name of the Charm the script is related to and updates the information in the specific charm's UI
    public void UpdateValues()
    {
        string charmName = gameObject.GetComponent<ShopTemplate>().name.text;
        currentlyOwned.text = CurrentCharms.OwnedCharms[charmName].ToString();
        string charmSign = CurrentCharms.CharmSign[charmName];
        if (charmSign == "+")
        {
            currentBuff.text = CurrentCharms.CharmSign[charmName] + CurrentCharms.CurrentBuff[charmName].ToString("f0");
        }
        else
        {
            currentBuff.text = CurrentCharms.CharmSign[charmName] + CurrentCharms.CurrentBuff[charmName].ToString("f1");
        }
    }
}
