using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Q_2 : Q_ParentClass
{
    //public override bool cutscene { get; set; } = true;//�ƾ��� �ִ� -> �θ� class���� true�̹Ƿ� �� �ڵ带 �� ���ָ� ������ ���µ�

    public BoxCollider Q_2_trigger;
    public BoxCollider Q_3_trigger;

    public BoxCollider light_end;

    public LightController lightController;
    
    public CutSceneController cutSceneController;
    public override void UpdateQuest()
    {

    }

    public override void NextQuest()
    {
        /*
        Q_2_trigger.enabled = false;
        Q_3_trigger.enabled = true;

        lightController.ChangeAllPointLightsExceptExcluded(Color.red);

        light_end.enabled = true;
        */

        

        cutSceneController.Scenechange = true;
    }

    

}
