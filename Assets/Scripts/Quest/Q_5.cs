using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Q_5 : Q_ParentClass
{
    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public OpenDoor OpenDoor;
    public GameObject Quest_6_Text;//gameObject�� �޾ƿ��� ���, �׳� �۵��� �ϱ� �ϰ����� getcomponent�� ���°� �� ����. Ȥ�� �̷��� Text�� �޾ƿ��� �͵� ������.
    public Start_myproject start_Myproject;
    
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
        start_Myproject.ShowMessage(Quest_6_Text, 3.0f); 
    }

    

}
