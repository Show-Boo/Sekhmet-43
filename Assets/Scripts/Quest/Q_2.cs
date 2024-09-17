using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Q_2 : Q_ParentClass
{
    //public override bool cutscene { get; set; } = true;//컷씬이 있다 -> 부모 class에서 true이므로 이 코드를 또 써주면 오류가 나는듯

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
