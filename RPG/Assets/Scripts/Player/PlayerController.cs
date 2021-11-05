using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ISavable
{
    private Vector2 input;
    private Character character;

    int miniScore = 0;
    int majorScore = 0;
    int miniTotalItems = 0;
    int majorTotalItems = 0;
    //int scene = 0;


    private void Awake()
    {
        character = GetComponent<Character>();
        //scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            input.x = SimpleInput.GetAxisRaw("Horizontal");
            input.y = SimpleInput.GetAxisRaw("Vertical");
            //input.x = Input.GetAxisRaw("Horizontal");
            //input.y = Input.GetAxisRaw("Vertical");

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

    public object CaptureState()
    {
        var saveData = new ImportantSaveData()
        {
            position = new float[] { transform.position.x, transform.position.y },
            miniQuizScore = miniScore,
            miniQuizItems = miniTotalItems,
            majorQuizScore = majorScore,
            majorQuizItems = majorTotalItems,
            //sceneIndex = scene
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (ImportantSaveData)state;

        miniScore = saveData.miniQuizScore;
        miniTotalItems = saveData.miniQuizItems;
        majorScore = saveData.majorQuizScore;
        majorTotalItems = saveData.majorQuizItems;
        transform.position = new Vector3(saveData.position[0], saveData.position[1]);
        //SceneManager.LoadSceneAsync(saveData.sceneIndex);

        Debug.Log("Mini Quiz" + miniScore + " / " + miniTotalItems);
        Debug.Log("Major Quiz" + majorScore + " / " + majorTotalItems);
    }


    public Character Char => character;

}

[Serializable]
public class ImportantSaveData
{
    public int miniQuizScore = 0;
    public int majorQuizScore = 0;
    public int miniQuizItems = 0;
    public int majorQuizItems = 0;
    //public int sceneIndex;
    public float[] position;

}
