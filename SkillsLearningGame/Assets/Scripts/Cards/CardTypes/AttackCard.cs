using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack card type
[CreateAssetMenu(menuName = "Cards/Type/Attack")]
public class Attack : CardType
{
    public override void OnSetType(CardUI ui)
    {
        base.OnSetType(ui);
    }
}
