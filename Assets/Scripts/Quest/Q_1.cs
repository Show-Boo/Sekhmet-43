using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_1 : Q_ParentClass
{
    public BoxCollider Q_1_trigger;
    public BoxCollider Q_2_trigger;
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가


    public override void UpdateQuest()
    {

    }

    public override void NextQuest()
    {
        Q_1_trigger.enabled = false;
        Q_2_trigger.enabled = true;

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