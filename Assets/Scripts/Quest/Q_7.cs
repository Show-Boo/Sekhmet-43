using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_7 : Q_ParentClass
{
    // Start is called before the first frame update
    public bool q_7_done = true;//일시적
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public override void UpdateQuest()
    {
        // 발전기 수리 완료 상태 확인
        if (GeneratorManager.Instance != null && GeneratorManager.Instance.engineIsAllFixed)
        {
            // 모든 발전기가 완료되었으면 퀘스트 완료
            QuestManager.Instance.CompleteObjective();
        }
    }

    public override void NextQuest()
    {
        StartCoroutine(ShowQuestUI());
    }


    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI 활성화
        yield return new WaitForSeconds(3); // 3초 대기
        questUI.SetActive(false); // UI 비활성화
    }

}
