using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_4 : Q_ParentClass
{
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가


    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public DisplayUIOnApproach displayUIOnApproach; //{ get; set; }-> 이걸 꼭 써야하나? ui system스크립트
    //public QuestManager QuestManager;//오류생겨서 get set 버림..
    public override void UpdateQuest()
    {
        if (displayUIOnApproach.isFirst)
        {
            QuestManager.CompleteObjective();
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
