using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_1 : Q_ParentClass
{

    //public override bool cutscene { get; set; } = true;//�ƾ��� �ִ� -> �θ� class���� true�̹Ƿ� �� �ڵ带 �� ���ָ� ������ ���µ�
    private BoxCollider Q_1_trigger;
    private BoxCollider Q_2_trigger;
    public override void UpdateQuest()
    {
        
    }

    public override void NextQuest()
    {
        Q_1_trigger = GetComponent<BoxCollider>();
        Q_2_trigger = GetComponent<BoxCollider>();

        Q_1_trigger.enabled = false;
        Q_2_trigger.enabled = true;
    }
}
