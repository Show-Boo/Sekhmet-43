using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_7 : Q_ParentClass
{
    // Start is called before the first frame update
    public bool q_7_done = true;//ÀÏ½ÃÀû

    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public override void UpdateQuest()
    {
        if (q_7_done)
        {
            QuestManager.CompleteObjective();
            
        }
    }

    public override void NextQuest()
    {

    }
}
