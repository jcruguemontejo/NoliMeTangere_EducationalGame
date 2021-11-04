using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string part;
    public int miniQuizScore;
    public int majorQuizScore;

    public Score(int miniQuizScore, int majorQuizScore)
    {
        this.miniQuizScore = miniQuizScore;
        this.majorQuizScore = majorQuizScore;
    }
}
