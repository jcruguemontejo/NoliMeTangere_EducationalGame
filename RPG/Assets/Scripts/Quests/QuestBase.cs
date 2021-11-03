using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Create a new Quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] string title;
    [SerializeField] string description;

    [SerializeField] string characterName;
    [SerializeField] Dialogue startDialogue;
    [SerializeField] Dialogue inProgressDialogue;
    [SerializeField] Dialogue completedDialogue;

    public string Title => title;
    public string Description => description;

    public string onCharName => characterName;

    public Dialogue onStartDialogue => startDialogue;

    public Dialogue onProgressDialogue => inProgressDialogue?.Lines?.Count > 0 ? inProgressDialogue : startDialogue;

    public Dialogue onCompleteDialogue => completedDialogue;

}
