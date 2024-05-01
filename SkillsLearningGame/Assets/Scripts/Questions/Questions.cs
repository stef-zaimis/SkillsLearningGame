using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates the question scriptableobject, which contains the question, difficulty, category and answers - store the correct answer first
[CreateAssetMenu(fileName = "Question", menuName = "Question")]
public class Questions : ScriptableObject
{
    public string question;
    public int difficulty;
    public string category;
    public Card.CardSubjectEnum cardSubject;
    public string link;
    public string[] answers;
}

