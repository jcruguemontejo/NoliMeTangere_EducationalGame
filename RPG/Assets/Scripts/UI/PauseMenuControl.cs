using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    List<Button> pauseMenuBtn;

    private void Awake()
    {
        pauseMenuBtn = pauseMenu.GetComponentsInChildren<Button>().ToList();
    }

    public void openPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        GameController.Instance.pauseGame(false);
    }

    public void HandleUpdate()
    {

    }
}
