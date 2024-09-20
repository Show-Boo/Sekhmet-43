using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_5 : Q_ParentClass
{
    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public OpenDoor OpenDoor;
    
    public override void UpdateQuest()
    {
        if (OpenDoor.isOpen)
        {
            QuestManager.CompleteObjective();
        }
    }

    // Update is called once per frame
    public override void NextQuest()
    {

    }
}
