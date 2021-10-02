using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizMaster : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject exclamationMark;
    [SerializeField] GameObject fov;
    [SerializeField] GameObject quiz;

    Character character;


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

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
