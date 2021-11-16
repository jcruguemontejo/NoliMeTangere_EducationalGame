using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Freeroam, Quizmode, Dialogue, Cutscene, Paused}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    [SerializeField] GameObject startMenuPanel;
    [SerializeField] GameObject btnPause;

    [SerializeField] GameObject virtualController;
    [SerializeField] GameObject interactButton;
    [SerializeField] GameObject coreGameObject;

    GameState gameState;
    GameState stateBeforePaused;


    public SceneDetails currentScene {get; private set; }

    public SceneDetails prevScene {get; private set; }

    PauseMenuControl pauseMenuControl;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        pauseMenuControl = GetComponent<PauseMenuControl>();

        if (SceneManager.GetActiveScene().name != "A Part1")
        {
            gameState = GameState.Cutscene;
            virtualController.SetActive(false);
            interactButton.SetActive(false);
        }
        else
        {
            gameState = GameState.Freeroam;
            virtualController.SetActive(true);
            interactButton.SetActive(true);
        }
    }

    private void Start()
    {
        DialogueManager.Instance.onShowDialogue += () =>
        {
            gameState = GameState.Dialogue;
        };

        DialogueManager.Instance.onCloseDialogue += () =>
        {
            if(gameState == GameState.Dialogue)
            {
                gameState = GameState.Freeroam;
            }
        };
    }

    public void newGame()
    {
        startMenuPanel.SetActive(false);
        gameState = GameState.Freeroam;
        btnPause.SetActive(true);
        SceneManager.LoadScene("school cutscene", LoadSceneMode.Single);
        Destroy(coreGameObject);
    }

    public void loadGame()
    {
        SavingSystem.i.Load("NMT_EG");
        gameState = GameState.Freeroam;
        startMenuPanel.SetActive(false);
        btnPause.SetActive(true);
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
        }
    }

    public void saveGame()
    {
        SavingSystem.i.Save("NMT_EG");
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

    public void quizResult(bool isMiniQuiz, int score, int totalItems)
    {
        Debug.Log("Game Controller Test");
        Debug.Log(isMiniQuiz+" "+score+" / "+totalItems);
        playerController.quizResultScore(isMiniQuiz, score, totalItems);
    }
}
