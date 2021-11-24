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

    [SerializeField] private bool isClicked;
    [SerializeField] Button interactButton;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        //dialogueBox.SetActive(false);
    }

    private void Start()
    {
        interactButton.onClick.AddListener(() => { isClicked = true; });
    }

    public bool isShowing { get; private set; }

    public IEnumerator ShowDialogue(Dialogue dialogue, string name)
    {
        yield return new WaitForEndOfFrame();

        onShowDialogue?.Invoke();
        isShowing = true;
        dialogueName.text = name;
        dialogueBox.SetActive(true);
        isClicked = false;
        
        if (isShowing)
        {
            foreach (var line in dialogue.Lines)
            {
                typeDialogue(line);
                yield return new WaitUntil(() => isClicked);
                isClicked = false;
            }
            dialogueBox.SetActive(false);
            isShowing = false;
            onCloseDialogue?.Invoke();
        }
        typeDialogue(dialogue.Lines[0]);
    }

    public void /*IEnumerator*/ typeDialogue(string line)
    {
        dialogueText.text = line;
        //foreach (var letter in line.ToCharArray())
        //{
        //    dialogueText.text += letter;
        //    yield return new WaitForSeconds(1f / lettersPerSecond);
        //}
    }
}
