using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUi;
    public ScoreManager scoreManager;

    void Start()
    {
        var scores = scoreManager.GetScores();

        for (int i = 0; i < scores.Count(); i++)
        {
            var row = Instantiate(rowUi, transform);
            row.rowName.text = "Part " + scores.ElementAt(i).id.ToString();
            row.miniQuiz.text = scores.ElementAt(i).miniQuiz.ToString();
            row.majorQuiz.text = scores.ElementAt(i).majorQuiz.ToString();
        }
    }

}
 