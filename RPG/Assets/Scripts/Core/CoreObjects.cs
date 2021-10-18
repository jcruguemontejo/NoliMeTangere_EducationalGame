using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreObjects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            Destroy(gameObject);
            Debug.Log("I am inside the if statement");
        }
    }
}
