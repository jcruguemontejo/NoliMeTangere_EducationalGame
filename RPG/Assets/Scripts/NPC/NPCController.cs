using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] new string name;
    [SerializeField] Dialogue dialogue;
    [SerializeField] QuestBase questToStart;
    [SerializeField] QuestBase questToComplete;
    [SerializeField] List<Vector2> movePattern;
    [SerializeField] float timeBetweenPattern;

    NPCState state;
    float idleTimer;
    int currentPattern = 0;

    Quest activeQuest;
    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public IEnumerator Interact(Transform initiator)
    {
        if (state == NPCState.Idle)
        {
            state = NPCState.Dialogue;
            character.lookDirection(initiator.position);

            if (questToStart != null)
            {
                //Start Quest
                activeQuest = new Quest(questToStart);
                yield return activeQuest.StartQuest();
                questToStart = null;
            }
            else if (activeQuest != null)
            {
                yield return DialogueManager.Instance.ShowDialogue(activeQuest.Base.onProgressDialogue, name);
                
                if (true)
                {

                }
            }
            else
            {
                yield return DialogueManager.Instance.ShowDialogue(dialogue, name);
            }

            idleTimer = 0f; 
            state = NPCState.Idle;
        } 
    }

    private void Update()
    {
        if(state == NPCState.Idle)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;

                if (movePattern.Count > 0)
                {
                    StartCoroutine(walk());
                }
            }
        }

        character.HandleUpdate();
    }

    IEnumerator walk()
    {
        state = NPCState.Walking;

        var oldPos = transform.position;
        
        yield return character.Move(movePattern[currentPattern]);
        
        if(transform.position != oldPos)
        {
            currentPattern = (currentPattern + 1) % movePattern.Count;
        }
        state = NPCState.Idle;
    }
}

public enum NPCState { Idle, Walking, Dialogue}
