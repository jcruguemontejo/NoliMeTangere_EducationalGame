using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField]string sceneName;
    private void OnEnable()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
