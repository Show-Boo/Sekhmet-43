using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_3 : Q_ParentClass
{
    // Start is called before the first frame update
    public BoxCollider Q_3_trigger;
    //public BoxCollider Q_4_trigger;
    
    public override void UpdateQuest()
    {

    }

    public override void NextQuest()
    {
        //Q_3_trigger = GetComponent<BoxCollider>();
        //Q_2_trigger = GetComponent<BoxCollider>();

        Q_3_trigger.enabled = false;
        cutsceneController.Scenechange = true;//여기가 맞나..
        //Q_3_trigger.enabled = true;


    }
}
