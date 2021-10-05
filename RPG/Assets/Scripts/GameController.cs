using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Freeroam, Quizmode, Dialogue, Cutscene, Paused}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState gameState;
    GameState stateBeforePaused;

    public SceneDetails currentScene {get; private set; }

    public SceneDetails prevScene {get; private set; }

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DialogueManager.instance.onShowDialogue += () =>
        {
            gameState = GameState.Dialogue;
        };

        DialogueManager.instance.onCloseDialogue += () =>
        {
            if(gameState == GameState.Dialogue)
            {
                gameState = GameState.Freeroam;
            }
        };

    }

    public void pauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePaused = gameState;
            gameState = GameState.Paused;
        }
        else
        {
            gameState = stateBeforePaused;
        }
    }

    private void Update()
    {
        if(gameState == GameState.Freeroam)
        {
            playerController.HandleUpdate();

            if (Input.GetKeyDown(KeyCode.E))
            {
                SavingSystem.i.Save("NMT_EG_2");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                SavingSystem.i.Load("NMT_EG_2");
            }
        }
        else if (gameState == GameState.Dialogue)
        {
            DialogueManager.instance.HandleUpdate();
        }
    }

    public void onEnterQuizMasterView(QuizMaster qm)
    {
        gameState = GameState.Cutscene;
        StartCoroutine(qm.triggerNPC(playerController));
    }

    public void startQuiz(GameObject quiz)
    {
        gameState = GameState.Quizmode;
        quiz.SetActive(true);
    }

    public void endQuiz(GameObject quiz)
    {
        gameState = GameState.Freeroam;
        quiz.SetActive(false);
    }

    public void setCurScene(SceneDetails curScene)
    {
        prevScene = currentScene;
        currentScene = curScene;
    }
}
