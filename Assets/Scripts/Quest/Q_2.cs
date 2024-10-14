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

    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가

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

        // 퀘스트 UI를 3초간 표시
        StartCoroutine(ShowQuestUI());
    }

    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI 활성화
        yield return new WaitForSeconds(3); // 3초 대기
        questUI.SetActive(false); // UI 비활성화
    }

}
