using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject btnPause;
    [SerializeField] GameObject coreGameObject;
    List<Button> pauseMenuBtn;

    private void Awake()
    {
        pauseMenuBtn = pauseMenu.GetComponentsInChildren<Button>().ToList();
    }

    public void openPauseMenu()
    {
        pauseMenu.SetActive(true);
        btnPause.SetActive(false);
    }
    public void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        btnPause.SetActive(true);
        GameController.Instance.pauseGame(false);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(coreGameObject);
    }

    public void saveGame()
    {

    }
}
