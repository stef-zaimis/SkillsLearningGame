using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Card subject class
public abstract class CardSubject : ScriptableObject
{
    public string subjectName;

    public virtual void OnSetSubject(CardUI ui)
    {
        /*
        Element s = Settings.GetResourceManager().subjectElement;
        CardUIProperties subject = ui.GetProperty(s);
        subject.text.text = subjectName;
        */
    }
}

