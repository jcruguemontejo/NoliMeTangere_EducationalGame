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

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool isShowing { get; private set; }

    public IEnumerator ShowDialogue(Dialogue dialogue, string name)
    {
        yield return new WaitForEndOfFrame();

        onShowDialogue?.Invoke();

        isShowing = true;
        dialogueName.text = name;
        dialogueBox.SetActive(true);

        foreach (var line in dialogue.Lines)
        {
            yield return typeDialogue(line);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        dialogueBox.SetActive(false);
        isShowing = false;
        onCloseDialogue?.Invoke();

        StartCoroutine(typeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate()
    {
        
    }

    public IEnumerator typeDialogue(string line)
    {
        dialogueText.text = "";
        foreach(var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}
