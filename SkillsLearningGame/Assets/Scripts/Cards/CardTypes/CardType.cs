using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Card types are scriptable objects defining either attack/damage cards or utility cards
public abstract class CardType : ScriptableObject
{
    public string typeName;
    public virtual void OnSetType(CardUI ui)
    {
        /*
        Element t = Settings.GetResourceManager().typeElement;
        CardUIProperties type = ui.GetProperty(t);
        type.text.text = typeName;
        */
    }
}
