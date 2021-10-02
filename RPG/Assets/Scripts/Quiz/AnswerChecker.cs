using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    public bool isCorrect = false;

    public QuizManager qm;

    public void checkButton()
    {
        Debug.Log("Button Working");
    }

    public void checkAns()
    {
        if (isCorrect)
        {
            Debug.Log("Correct");
            qm.correctAnswer();
        }
        else
        {
            Debug.Log("Wrong");
            qm.wrongAnswer();
        }
    }

}
