using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This class manages HP bars
public class EntityHPBar : MonoBehaviour
{
    public TMP_Text hpText;
    public Image blockImage;
    public TMP_Text blockText;
    public Slider hpBarSlider;

    // Display the block icon and amount over the HP Bar
    public void ShowBlock(int currentBlock)
    {
        if (currentBlock > 0)
        {
            blockText.text = currentBlock.ToString();
            blockImage.gameObject.SetActive(true);
            blockText.gameObject.SetActive(true);

        }
        else
        {
            blockImage.gameObject.SetActive(false);
            blockImage.gameObject.SetActive(false);
            currentBlock = 0;
        }
    }

    // Display your current HP and your max HP as a slider
    public void ShowHP(int currentHP)
    {
        hpText.text = $"{currentHP}/{hpBarSlider.maxValue}";
        hpBarSlider.value = currentHP;
    }
}
