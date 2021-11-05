using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector playable;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Trigger"+playable);
    }
}
