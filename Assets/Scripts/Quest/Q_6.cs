using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_6 : Q_ParentClass
{
    // Start is called before the first frame update
    //그 ray를..끄고싶은데..
    public bool Quest6_clear = true;//일시적으로 바꾸는게 왜 안통하지,,,,
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가


    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public Q_5 q_5;
    

    public override void UpdateQuest()
    {
        if (Quest6_clear)
        {
            QuestManager.CompleteObjective();
        }
    }

    public override void NextQuest()
    {
        q_5.Quest_6_Text.SetActive(false);//ui 비활성화
        StartCoroutine(ShowQuestUI());
    }


    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI 활성화
        yield return new WaitForSeconds(3); // 3초 대기
        questUI.SetActive(false); // UI 비활성화
    }

}
