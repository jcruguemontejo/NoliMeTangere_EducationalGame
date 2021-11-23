using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoadData : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("Loading Game");
        string sceneName = PlayerPrefs.GetString(key: "Save_Scene");
        SceneManager.LoadScene(sceneName: sceneName);

        GameData.gameLoaded = true;
        
        Debug.Log("Game Loaded");
    }

    public void SaveGame()
    {
        Debug.Log("Saving Game");
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(key: "Save_Scene", value: currentSceneName);

        PlayerPrefs.SetFloat(key: "Save_Player_Postion_X", value: GameData.playerPosX);
        PlayerPrefs.SetFloat(key: "Save_Player_Postion_Y", value: GameData.playerPosY);

        Debug.Log("Game Saved");
    }

}
