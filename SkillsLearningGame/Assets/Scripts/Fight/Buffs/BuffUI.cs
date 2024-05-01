using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class is responsible for displaying buffs in the game, giving them an image and a text showing the buff/debuff amount
public class BuffUI : MonoBehaviour
{
    public Image buffImage;
    public TMP_Text buffText;

    // This function simply displays a given buff/debuff
    public void DisplayBuff(Buffs buff)
    {
        buffImage.sprite = buff.buffImage;
        buffText.text = buff.buffAmount.ToString();
    }
}