using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCharmInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text charmName;
    [SerializeField]
    private TMP_Text numOwned;
    [SerializeField]
    private TMP_Text currBuff;

    // Sets all the information on the charm shop to the charm corresponding to the inputted name.
    public void UpdateInformation(string charm)
    {
        charmName.text = charm;
        string charmSign = CurrentCharms.CharmSign[charm];
        if (charmSign == "+")
        {
            currBuff.text = charmSign + CurrentCharms.CurrentBuff[charm].ToString("f0");
        }
        else
        {
            currBuff.text = charmSign + CurrentCharms.CurrentBuff[charm].ToString("f1");
        }
        numOwned.text = CurrentCharms.OwnedCharms[charm].ToString();
    }
}
