using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_4 : Q_ParentClass
{
    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public DisplayUIOnApproach displayUIOnApproach; //{ get; set; }-> �̰� �� ����ϳ�? ui system��ũ��Ʈ
    //public QuestManager QuestManager;//�������ܼ� get set ����..
    public override void UpdateQuest()
    {
        if (displayUIOnApproach.isFirst)
        {
            QuestManager.CompleteObjective();
        }
    }

    public override void NextQuest()
    {
        
    }
}
