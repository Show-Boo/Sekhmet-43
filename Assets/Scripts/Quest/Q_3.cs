using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_3 : Q_ParentClass
{
    // Start is called before the first frame update
    public BoxCollider Q_3_trigger;
    //public BoxCollider Q_4_trigger;
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가
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
