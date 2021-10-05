using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QnA> QnA;
    public GameObject[] choices;
    public int currQuestion;

    public Text QText;
    public Text scoreCounter;

    public GameObject QuizHolder;
    public GameObject QuizPanel;
    public GameObject QuizOver;

    public GameObject quizMasterFov;

    int totalItems = 0;
    public int score = 0;

    public QuizMaster quizMaster;

    private void Awake()
    {
        totalItems = QnA.Count;
        QuizOver.SetActive(false);
        QuizPanel.SetActive(true);
        generateQuestion();
    }

    public void doneButton()
    {
        GameController.Instance.endQuiz(QuizHolder);
        quizMasterFov.SetActive(false);
        quizMaster.quizDone(true);
    }

    void quizOver()
    {
        QuizPanel.SetActive(false);
        QuizOver.SetActive(true);
        scoreCounter.text = score + " / " + totalItems;
    }

    public void correctAnswer()
    {
        score++;
        QnA.RemoveAt(currQuestion);
        generateQuestion();
    }

    public void wrongAnswer()
    {
        QnA.RemoveAt(currQuestion);
        generateQuestion();
    }

    void setAnswer()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<AnswerChecker>().isCorrect = false;
            choices[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currQuestion].Answers[i];

            if (QnA[currQuestion].CorrectAnswer == i + 1)
            {
                choices[i].GetComponent<AnswerChecker>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currQuestion = Random.Range(0, QnA.Count);
            QText.text = QnA[currQuestion].Question;
            setAnswer();
        }
        else
        {
            Debug.Log("Done");
            quizOver();
        }

    }
}
