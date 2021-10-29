using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMaster : MonoBehaviour, Interactable, ISavable
{
    [SerializeField] new string name;
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue dialogueAfter;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject quizMasterFov;
    [SerializeField] GameObject quiz;

    Character character;

    public bool isQuizFinish = false;

    public static QuizMaster Instance { get; private set; }

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        setFovRotation(character.anim.defaultDir);
    }

    public IEnumerator Interact(Transform initiator)
    {
        character.lookDirection(initiator.position);

        if (!isQuizFinish)
        {
            yield return DialogueManager.Instance.ShowDialogue(dialogue, name);
            Debug.Log("Start Quiz");
            GameController.Instance.startQuiz(quiz);
        }
        else
        {
            yield return DialogueManager.Instance.ShowDialogue(dialogueAfter, name);
        }
    }

    public IEnumerator triggerNPC(PlayerController player)
    {
        //show exclamation mark
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclamationMark.SetActive(false);

        //walk towards player
        var diff = player.transform.position - transform.position;
        var moveDiff = diff - diff.normalized;
        moveDiff = new Vector2(Mathf.Round(moveDiff.x), Mathf.Round(moveDiff.y));

        yield return character.Move(moveDiff);

        //show start dialog
        yield return DialogueManager.Instance.ShowDialogue(dialogue, name);
        Debug.Log("Start Quiz");
        GameController.Instance.startQuiz(quiz);
        
    }

    public void setFovRotation(faceDir dir)
    {
        float angle = 0f;

        if (dir == faceDir.Right)
        {
            angle = 90f;
        }
        else if (dir == faceDir.Left)
        {
            angle = 270f;
        }
        else if (dir == faceDir.Up)
        {
            angle = 180f;
        }

        quizMasterFov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void quizDone(bool isDone)
    {
        isQuizFinish = isDone;
    }

    public object CaptureState()
    {
        return isQuizFinish;
    }

    public void RestoreState(object state)
    {
        isQuizFinish = (bool)state;
        if (isQuizFinish)
        {
            quizMasterFov.gameObject.SetActive(false);
        }
        else
        {
            quizMasterFov.gameObject.SetActive(true);
        }
    }
}

/*, int score, int totalItems*/

//PlayerController.Instance.setQuizData(score, isMiniQuiz, totalItems);

//quizMasterFov.transform.eulerAngles = new Vector3(0f, 0f, angle);
//    }

//    public void quizDone(bool isDone)
//{
//    isQuizFinish = isDone;
//}

//public object CaptureState()
//{
//    return isQuizFinish;
//}

//public void RestoreState(object state)
//{
//    isQuizFinish = (bool)state;
//    if (isQuizFinish)
//    {
//        quizMasterFov.gameObject.SetActive(false);
//    }
//}


