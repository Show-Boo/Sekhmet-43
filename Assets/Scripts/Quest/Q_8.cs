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
            QuestManager.CompleteObjective();//���ּ� ž��-> ����Ʈ ��
        }
    }

    public override void NextQuest()
    {

    }
}
