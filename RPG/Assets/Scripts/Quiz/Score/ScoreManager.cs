using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreData sd;

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        sd = new ScoreData();
        Debug.Log(PlayerPrefs.HasKey("scores") ? "ScoreManager: Found scores in PlayerPrefs" : "ScoreManager: No scores found in PlayerPrefs");
        if (PlayerPrefs.HasKey("scores"))
        {
            string json = PlayerPrefs.GetString("scores", "{}");
            sd = JsonUtility.FromJson<ScoreData>(json);
        }
        else
        {
            sd = new ScoreData();
            AddScore(new Score(2, 0, 0));
            AddScore(new Score(5, 0, 0));
            AddScore(new Score(4, 0, 0));
            AddScore(new Score(3, 0, 0));
            AddScore(new Score(1, 0, 0));
            AddScore(new Score(6, 0, 0));
        }
    }

    public void scoreToScoreboard()
    {
        AddScore(new Score(GameData.part, GameData.miniQuizScore, GameData.majorQuizScore));
    }

    public IEnumerable<Score> GetScores()
    {
        return sd.scores.OrderBy(x => x.id);
    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }

    private void OnDestroy()
    {
        Savescore();
    }

    public void Savescore()
    {
        string json = JsonUtility.ToJson(sd);
        //Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
    }

}

