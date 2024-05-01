using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic card class containing all the card attributes
[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int cardCost;
    public int effectAmount;
    public string cardDescription;
    public int buffAmount;
    public CardTypeEnum cardTypeEnum;
    public enum CardTypeEnum { Attack, Utility }
    public CardSubjectEnum cardSubject;
    public enum CardSubjectEnum { AI, DS, Cloud, TI }
    public Sprite cardImage;
    public CardTargetType cardTargetType;
    public enum CardTargetType { Player, Enemy };
}