using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMaster : MonoBehaviour, ISavable
{
    [SerializeField] new string name;
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject quizMasterFov;
    [SerializeField] GameObject quiz;

    Character character;

    bool isQuizDone = false;
    int score = 0;
    int quizItems = 0;

    public static QuizMaster Instance { get; private set; }

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        setFovRotation(character.anim.defaultDir);
    }

    public IEnumerator triggerNPC(PlayerController player)
    {
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclamationMark.SetActive(false);

        var diff = player.transform.position - transform.position;
        var moveDiff = diff - diff.normalized;
        moveDiff = new Vector2(Mathf.Round(moveDiff.x), Mathf.Round(moveDiff.y));

        yield return character.Move(moveDiff);

        StartCoroutine(DialogueManager.instance.ShowDialogue(dialogue, name, () => 
        {
            Debug.Log("Start Quiz");
            GameController.Instance.startQuiz(quiz);
        }));
    }

    public void setFovRotation(faceDir dir)
    {
        float angle = 0f;

        if (dir == faceDir.Right)
        {
            angle = 90f;
        } else if (dir == faceDir.Left)
        {
            angle = 270f;
        } else if (dir == faceDir.Up)
        {
            angle = 180f;
        }

        quizMasterFov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void quizDone(bool isDone, int score, int totalItems)
    {
        isQuizDone = isDone;
        this.score = score;
        quizItems = totalItems;
    }

    public object CaptureState()
    {
        var saveData = new QuizSaveData()
        {
            quizFinish = isQuizDone,
            quizScore = score,
            totalQuizItems = quizItems
        };
        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (QuizSaveData)state;

        score = saveData.quizScore;
        quizItems = saveData.totalQuizItems;
        isQuizDone = saveData.quizFinish;
        if (isQuizDone)
        {
            quizMasterFov.gameObject.SetActive(false);
        }
        Debug.Log(score+" / "+quizItems+" "+isQuizDone);
    }
}

[Serializable]
public class QuizSaveData
{
    public bool quizFinish = false;
    public int quizScore;
    public int totalQuizItems;
}
