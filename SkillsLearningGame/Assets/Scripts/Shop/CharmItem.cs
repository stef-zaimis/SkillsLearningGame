using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class CharmItem : ScriptableObject
{
    public string name;
    public string description;
    public int cost;
    // -1 for maxAllowed means that an infinite amount is allowed
    public int maxAllowed;
}
