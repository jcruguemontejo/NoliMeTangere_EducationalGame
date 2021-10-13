using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;
    
    public bool isLoaded { get; private set; }

    List<SavableEntity> savableEntity;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")
        {
            Debug.Log($"Entered {gameObject.name}");
            LoadScene();
            GameController.Instance.setCurScene(this);

            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            var prevScene = GameController.Instance.prevScene;
            if (prevScene != null)
            {
                var prevLodedScenes = prevScene.connectedScenes;
                foreach(var scene in prevLodedScenes)
                {
                    if(!connectedScenes.Contains(scene) && scene != this)
                    {
                        scene.unloadScene();
                    }
                }

                if (!connectedScenes.Contains(prevScene))
                {
                    prevScene.unloadScene();
                }
                    
            }
        }
    }
    public void LoadScene()
    {
        if(!isLoaded)
        {
            var operation = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;

            operation.completed += (AsyncOperation op) =>
            {
                savableEntity = GetSavableEntities();
                SavingSystem.i.RestoreEntityStates(savableEntity);
            };
        }
    }

    public void unloadScene()
    {
        if (isLoaded)
        {
            SavingSystem.i.CaptureEntityStates(savableEntity);

            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    List<SavableEntity> GetSavableEntities()
    {

        var currScene = SceneManager.GetSceneByName(gameObject.name);
        var savableEntities = FindObjectsOfType<SavableEntity>().Where(x => x.gameObject.scene == currScene).ToList();
        return savableEntities;
    }
}

