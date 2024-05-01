using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterManager : MonoBehaviour
{
    private QuizBattleQuestionControl questionControl;

    void Awake()
    {
        questionControl = FindObjectOfType<QuizBattleQuestionControl>();
        // Initialise selected subjects as all subjects
        questionControl.selectedSubjects = new List<Card.CardSubjectEnum> {
            Card.CardSubjectEnum.TI,
            Card.CardSubjectEnum.AI,
            Card.CardSubjectEnum.DS,
            Card.CardSubjectEnum.Cloud
        };
    }

    public void selectAI()
    {
        if (questionControl.selectedSubjects.Contains(Card.CardSubjectEnum.AI))
        {
            questionControl.selectedSubjects.Remove(Card.CardSubjectEnum.AI);
            Debug.Log(string.Join(", ", questionControl.selectedSubjects));
            return;
        }
        questionControl.selectedSubjects.Add(Card.CardSubjectEnum.AI);
        Debug.Log(string.Join(", ", questionControl.selectedSubjects));
    }

    public void selectTI()
    {
        if (questionControl.selectedSubjects.Contains(Card.CardSubjectEnum.TI))
        {
            questionControl.selectedSubjects.Remove(Card.CardSubjectEnum.TI);
            Debug.Log(string.Join(", ", questionControl.selectedSubjects));
            return;
        }
        questionControl.selectedSubjects.Add(Card.CardSubjectEnum.TI);
        Debug.Log(string.Join(", ", questionControl.selectedSubjects));
    }

    public void selectDS()
    {
        if (questionControl.selectedSubjects.Contains(Card.CardSubjectEnum.DS))
        {
            questionControl.selectedSubjects.Remove(Card.CardSubjectEnum.DS);
            Debug.Log(string.Join(", ", questionControl.selectedSubjects));
            return;
        }
        questionControl.selectedSubjects.Add(Card.CardSubjectEnum.DS);
        Debug.Log(string.Join(", ", questionControl.selectedSubjects));
    }

    public void selectCloud()
    {
        if (questionControl.selectedSubjects.Contains(Card.CardSubjectEnum.Cloud))
        {
            questionControl.selectedSubjects.Remove(Card.CardSubjectEnum.Cloud);
            Debug.Log(string.Join(", ", questionControl.selectedSubjects));
            return;
        }
        questionControl.selectedSubjects.Add(Card.CardSubjectEnum.Cloud);
        Debug.Log(string.Join(", ", questionControl.selectedSubjects));
    }
}
