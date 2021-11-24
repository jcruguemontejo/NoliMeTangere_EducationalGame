using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QnA> QnA;
    public GameObject[] choices;
    public bool isMiniQuiz = false;
    public int part = 1;

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
        if (isMiniQuiz)
        {
            ScoreManager.Instance.sd = new ScoreData();
            GameData.part = part;
            GameData.miniQuizScore = score;
            ScoreManager.Instance.scoreToScoreboard();
        }
        else
        {
            ScoreManager.Instance.sd = new ScoreData();
            GameData.part = part;
            GameData.majorQuizScore = score;
            ScoreManager.Instance.scoreToScoreboard();
        }
        GameController.Instance.endQuiz(QuizHolder);
        quizMasterFov.SetActive(false);
    }

    void quizOver()
    {
        QuizPanel.SetActive(false);
        QuizOver.SetActive(true);
        score = score / totalItems;
        scoreCounter.text = score.ToString();
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

