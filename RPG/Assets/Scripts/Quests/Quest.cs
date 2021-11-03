using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestBase Base { get; private set; }

    public QuestStatus Status { get; private set; }

    public Quest(QuestBase questBase)
    {
        Base = questBase;
    }

    public IEnumerator StartQuest(string name)
    {
        Status = QuestStatus.Started;
        yield return DialogueManager.Instance.ShowDialogue(Base.onStartDialogue, name);
    }

    public IEnumerator CompleteQuest(string name)
    {
        Status = QuestStatus.Completed;
        yield return DialogueManager.Instance.ShowDialogue(Base.onCompleteDialogue, name);
    }
}

public enum QuestStatus { None, Started, Completed}
