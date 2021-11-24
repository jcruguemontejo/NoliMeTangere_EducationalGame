using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationPortal : MonoBehaviour, PlayerTriggerable
{
    [SerializeField] destID destPortal;
    [SerializeField] Transform spawnPoint;

    PlayerController player;

    public void onPlayerTriggered(PlayerController player)
    {
        this.player = player;
        StartCoroutine(Teleport());
    }

    Fader fader;
    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }

    IEnumerator Teleport()
    {
        GameController.Instance.pauseGame(true);

        yield return fader.fadeIn(0.3f);
        Debug.Log("Switching Scenes!");

        var desPortal = FindObjectsOfType<LocationPortal>().First(x => x != this && x.destPortal == this.destPortal);
        player.Char.setPositionAndSnapToTile(desPortal.SpawnPoint.position);
        
        yield return fader.fadeOut(0.3f);
        GameController.Instance.pauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;

}
