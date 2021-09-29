using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Portal : MonoBehaviour, PlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] destID destPortal;
    [SerializeField] Transform spawnPoint;

    PlayerController player;

    public void onPlayerTriggered(PlayerController player)
    {
        this.player = player;
        StartCoroutine(switchScene());
    }

    Fader fader;
    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }

    IEnumerator switchScene()
    {
        DontDestroyOnLoad(gameObject);

        GameController.Instance.pauseGame(true);

        yield return fader.fadeIn(0.3f);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        Debug.Log("Switching Scenes!");

        var desPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destPortal == this.destPortal);

        player.Char.setPositionAndSnapToTile(desPortal.SpawnPoint.position);
        yield return fader.fadeOut(0.3f);
        GameController.Instance.pauseGame(false);

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;

}

public enum destID { A, B, C, D, E }
