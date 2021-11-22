using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene("1 school cutscene", LoadSceneMode.Single);
    }
}
