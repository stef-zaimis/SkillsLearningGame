using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains the necessary attributes for a single enemy action
[System.Serializable]
public class EnemyActions
{
    public ActionType actionType;
    public enum ActionType { Attack, Block, Strengthen, Weaken, Vulnerable }
    public int effectAmount;
    public int chance;
    public Sprite icon;
}