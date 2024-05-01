using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a basic buff struct containing the necessary elements for a buff action
[System.Serializable]
public struct Buffs
{
    public Type type;
    public enum Type { strength, weakness, vulnerable }
    public Sprite buffImage;
    public int buffAmount;
    public BuffUI buffUI;
}