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
        if (SceneManager.GetActiveScene().name.Contains("Start Menu"))
        {
            virtualController.SetActive(false);
            interactButton.SetActive(false);
            btnPause.SetActive(false);
            startMenuPanel.SetActive(true);
        }
        else if (!SceneManager.GetActiveScene().name.Contains("A Part") && !SceneManager.GetActiveScene().name.Contains("Start Menu"))
        {
            gameState = GameState.Cutscene;
            virtualController.SetActive(false);
            interactButton.SetActive(false);
            btnPause.SetActive(false);
            Destroy(coreGameObject);
        }
        else if(SceneManager.GetActiveScene().name.Contains("A Part"))
        {
            gameState = GameState.Freeroam;
            virtualController.SetActive(true);
            interactButton.SetActive(true);
            btnPause.SetActive(true);
            startMenuPanel.SetActive(false);
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
        SceneManager.LoadScene("1 school cutscene", LoadSceneMode.Single);
    }

    public void loadGame()
    {
        SavingSystem.i.Load("NMT_EG");
        gameState = GameState.Freeroam;
        startMenuPanel.SetActive(false);
        btnPause.SetActive(true);
        GameData.gameLoaded = true;
        Debug.Log("Loading Game");
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
        if (SceneManager.GetActiveScene().name.Contains("Start Menu"))
        {
            virtualController.SetActive(false);
            interactButton.SetActive(false);
            btnPause.SetActive(false);
            startMenuPanel.SetActive(true);
        }
        else if (!SceneManager.GetActiveScene().name.Contains("A Part") && !SceneManager.GetActiveScene().name.Contains("Start Menu"))
        {
            virtualController.SetActive(false);
            interactButton.SetActive(false);
            btnPause.SetActive(false);
            Destroy(coreGameObject);
        }

        if (gameState == GameState.Freeroam)
        {
            playerController.HandleUpdate();
        }
        
        if (SceneManager.GetActiveScene().name.Contains("A Part") && gameState == GameState.Freeroam)
        {
            virtualController.SetActive(true);
            interactButton.SetActive(true);
            btnPause.SetActive(true);
            startMenuPanel.SetActive(false);
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
