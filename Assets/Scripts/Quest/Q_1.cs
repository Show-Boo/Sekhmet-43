using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_1 : Q_ParentClass
{

    //public override bool cutscene { get; set; } = true;//컷씬이 있다 -> 부모 class에서 true이므로 이 코드를 또 써주면 오류가 나는듯
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
