using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicators : MonoBehaviour
{
    private TMP_Text dmgText;
    private Rigidbody2D rigidBody;

    public float yVelocityRange = 7f;
    public float xVelocityRange = 100f;
    public float lifetime = 0.8f;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        dmgText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        float xVel = Random.Range(-xVelocityRange, xVelocityRange);
        float yVel = Random.Range(yVelocityRange, 3 * yVelocityRange);
        rigidBody.velocity = new Vector2(xVel, yVel);
        Destroy(gameObject, lifetime);
    }

    public void SetMessage(string message, int sign)
    {
        if (sign == 1)
        {
            dmgText.SetText("+" + message);
            dmgText.color = Color.green;
        }
        else
        {
            dmgText.SetText("-" + message);
            dmgText.color = Color.red;
        }
    }
}