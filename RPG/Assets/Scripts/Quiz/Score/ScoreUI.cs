using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUi;
    public ScoreManager scoreManager;

    public static int miniQuizScoreAverage = 0;
    public static int majorQuizScoreAverage = 0;

    void Start()
    {
        scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        scoreManager.AddScore(new Score(100, 100));
        //if (QuizScores.whatPart == "Part1")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}
        //else if (QuizScores.whatPart == "Part2")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}
        //else if (QuizScores.whatPart == "Part3")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}
        //else if (QuizScores.whatPart == "Part4")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}
        //else if (QuizScores.whatPart == "Part5")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}
        //else if (QuizScores.whatPart == "Finale")
        //{
        //    scoreManager.AddScore(new Score(QuizScores.miniQuizScore, QuizScores.majorQuizScore));
        //}



        var scores = scoreManager.GetScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
            row.part.text = (i + 1).ToString();
            row.miniQuizScore.text = scores[i].miniQuizScore.ToString();
            row.majorQuizScore.text = scores[i].majorQuizScore.ToString();
        }
        scoreManager.AddScore(new Score(miniQuizScoreAverage, majorQuizScoreAverage));
    }

}
 