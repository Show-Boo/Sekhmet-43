using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_6 : Q_ParentClass
{
    // Start is called before the first frame update
    //그 ray를..끄고싶은데..
    public bool Quest6_clear = false;

    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public Q_5 q_5;
    

    public override void UpdateQuest()
    {
        if (Quest6_clear)
        {
            QuestManager.CompleteObjective();
        }
    }

    public override void NextQuest()
    {
        q_5.Quest_6_Text.SetActive(false);//ui 비활성화
        
    }
}
