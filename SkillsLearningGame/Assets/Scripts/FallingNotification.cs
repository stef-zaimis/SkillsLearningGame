using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallingNotification : MonoBehaviour
{
    private TMP_Text text;
    private Rigidbody2D rigidBody;
    public float lifetime = 1f;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = new Vector2(0, 0);
        Destroy(gameObject, lifetime);
    }

    public void SetText(string newText, Color color)
    {
        text.SetText(newText);
        text.color = color;
    }
}
