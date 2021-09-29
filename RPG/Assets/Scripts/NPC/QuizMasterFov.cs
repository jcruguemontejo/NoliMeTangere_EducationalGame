using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMasterFov : MonoBehaviour, PlayerTriggerable
{
    public void onPlayerTriggered(PlayerController player)
    {
        GameController.Instance.onEnterQuizMasterView(GetComponentInParent<QuizMaster>());
    }
}
