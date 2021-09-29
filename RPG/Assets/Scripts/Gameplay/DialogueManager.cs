using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueName;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;

    public event Action onShowDialogue;
    public event Action onCloseDialogue;

    public static DialogueManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    Dialogue dialogue;
    Action dialogueDone;
    int currentLine = 0;
    bool isTyping;

    public bool isShowing { get; private set; }

    public IEnumerator ShowDialogue(Dialogue dialogue, string name, Action onFinish=null)
    {
        yield return new WaitForEndOfFrame();

        onShowDialogue?.Invoke();

        isShowing = true;
        this.dialogue = dialogue;
        dialogueDone = onFinish;
        dialogueName.text = name;
        dialogueBox.SetActive(true);
        StartCoroutine(typeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(typeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                isShowing = false;
                dialogueBox.SetActive(false);
                dialogueDone?.Invoke();
                onCloseDialogue?.Invoke();
            }
        }
    }

    public IEnumerator typeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
