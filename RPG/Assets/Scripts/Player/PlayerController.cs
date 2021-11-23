using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Character character;

    int miniScore = 0;
    int majorScore = 0;
    int miniTotalItems = 0;
    int majorTotalItems = 0;


    private void Awake()
    {
        character = GetComponent<Character>();
        if (GameData.gameLoaded)
        {
            if (SceneManager.GetActiveScene().name.Contains("A Part"))
            {
                transform.position = new Vector3(PlayerPrefs.GetFloat(key: "Save_Player_Postion_X"), PlayerPrefs.GetFloat(key: "Save_Player_Postion_Y"));
                //miniScore = GameData.miniQuizScore;
                //miniTotalItems = GameData.miniQuizItems;
                //majorScore = GameData.majorQuizScore;
                //majorTotalItems = GameData.majorQuizItems;
            }
        }
        
    }
    private void Update()
    { 
        GameData.playerPosX = transform.position.x;
        GameData.playerPosY = transform.position.y;
    }

    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            input.x = SimpleInput.GetAxisRaw("Horizontal");
            input.y = SimpleInput.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, onMoveOver));
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Interact());
        }
    }

    public void startInteraction()
    {
        StartCoroutine(Interact());
    }

    IEnumerator Interact()
    {
        var facingDir = new Vector3(character.anim.moveX, character.anim.moveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.interactableLayer);
        if (collider != null)
        {
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    private void onMoveOver()
    {
        var triggerableCollider = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.offSetY), 0.2f, GameLayers.i.triggerableLayers);

        foreach (var collider in triggerableCollider)
        {
            var triggerable = collider.GetComponent<PlayerTriggerable>();

            if (triggerable != null)
            {
                character.anim.isMoving = false;
                triggerable.onPlayerTriggered(this);
                break;
            }
        }
    }

    public void quizResultScore(bool isMiniQuiz, int score, int items)
    {
        if (isMiniQuiz)
        {
            miniScore += score;
            miniTotalItems += items;
            QuizScores.miniQuizScore = miniScore;
            QuizScores.miniQuizItems = miniTotalItems;
            Debug.Log("Mini Quiz Score: " + miniScore + " / " + miniTotalItems);
        }
        else
        {
            majorScore = score;
            majorTotalItems = items;
            QuizScores.majorQuizScore = majorScore;
            QuizScores.majorQuizItems = majorTotalItems;
            Debug.Log("Major Quiz Score: " + majorScore + " / " + majorTotalItems);
        }

        QuizScores.majorQuizScore = majorScore;
    }
    public Character Char => character;

}
