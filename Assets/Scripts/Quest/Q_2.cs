using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_2 : Q_ParentClass
{
    //public override bool cutscene { get; set; } = true;//�ƾ��� �ִ� -> �θ� class���� true�̹Ƿ� �� �ڵ带 �� ���ָ� ������ ���µ�

    private BoxCollider Q_2_trigger;
    private BoxCollider Q_3_trigger;
    public override void UpdateQuest()
    {

    }

    public override void NextQuest()
    {
        Q_3_trigger = GetComponent<BoxCollider>();
        Q_2_trigger = GetComponent<BoxCollider>();

        Q_2_trigger.enabled = false;
        Q_3_trigger.enabled = true;
    }
}
