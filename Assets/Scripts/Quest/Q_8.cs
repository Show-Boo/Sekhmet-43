using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_8 : Q_ParentClass
{
    public bool q_8_done = false;
    public override void UpdateQuest()
    {
        if (q_8_done)
        {
            QuestManager.CompleteObjective();//우주선 탑승-> 퀘스트 끝
        }
    }

    public override void NextQuest()
    {

    }
}
