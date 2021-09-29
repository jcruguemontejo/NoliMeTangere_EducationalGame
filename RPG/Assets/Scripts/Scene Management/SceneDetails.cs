using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;
    
    public bool isLoaded { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")
        {
            Debug.Log($"Entered {gameObject.name}");
            LoadScene();
            GameController.Instance.setCurScene(this);

            // Load Connected Scences
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            // Unload Scenes not connected
            if (GameController.Instance.prevScene != null)
            {
                var prevLodedScenes = GameController.Instance.prevScene.connectedScenes;
                foreach(var scene in prevLodedScenes)
                {
                    if(!connectedScenes.Contains(scene) && scene != this)
                    {
                        scene.unloadScene();
                    }
                }
            }
        }
    }
    public void LoadScene()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
            
        }
    }

    public void unloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;

        }
    }
}

