using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class level manager is obsolete now
public class LevelManager : MonoBehaviour
{

}

// The level struct handles the different level types there can be
[System.Serializable]
public struct Level
{
    public Type levelType;
    public enum Type { fight, bossFight };
    public Sprite levelEncounterSprite;
}
