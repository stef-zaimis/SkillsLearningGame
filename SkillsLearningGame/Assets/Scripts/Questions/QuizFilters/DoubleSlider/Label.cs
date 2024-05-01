using TMPro;
using UnityEngine;

public class Label : MonoBehaviour
{
    public string Text
    {
        get { return _label.text; }
        set { _label.text = value; }
    }

    private TextMeshProUGUI _label;

    private void Awake()
    {
        if (!TryGetComponent(out _label))
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("Missing Text Component");
#endif
        }
    }
}