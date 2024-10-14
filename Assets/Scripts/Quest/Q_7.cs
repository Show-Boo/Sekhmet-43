using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_7 : Q_ParentClass
{
    // Start is called before the first frame update
    public bool q_7_done = true;//�Ͻ���
    public GameObject questUI; // ����Ʈ UI�� ��Ÿ���� GameObject �߰�
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public override void UpdateQuest()
    {
        if (q_7_done)
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
        questUI.SetActive(true); // UI Ȱ��ȭ
        yield return new WaitForSeconds(3); // 3�� ���
        questUI.SetActive(false); // UI ��Ȱ��ȭ
    }
}
