using System;

[Serializable]
public class Score
{
    public int id;
    public int miniQuiz;
    public int majorQuiz;

    public Score(int id, int miniQuiz, int majorQuiz)
    {
        this.id = id;
        this.miniQuiz = miniQuiz;
        this.majorQuiz = majorQuiz;
    }
}


