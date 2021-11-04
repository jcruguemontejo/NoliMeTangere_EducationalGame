using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;

    private void Awake()
    {
        sd = new ScoreData();
    }

    public IEnumerable<Score> GetScores()
    {
        return sd.scores;
    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }

}
