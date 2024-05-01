using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void OnClick();

    void OnHighlight();

    void ResetSize();

    void ResetPosition();

    void Elevate();

    void Magnify();
}
