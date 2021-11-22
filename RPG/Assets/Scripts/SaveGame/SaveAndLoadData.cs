using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoadData : MonoBehaviour
{
    public void LoadGame()
    {
        string sceneName = PlayerPrefs.GetString(key: "Save_Scene");
        SceneManager.LoadScene(sceneName: sceneName);
    }

    public void SaveGame()
    {   
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(key: "Save_Scene", value: currentSceneName);
    }
}
