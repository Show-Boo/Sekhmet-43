using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_1 : Q_ParentClass
{
    public BoxCollider Q_1_trigger;
    public BoxCollider Q_2_trigger;
    public GameObject questUI; // ����Ʈ UI�� ��Ÿ���� GameObject �߰�


    public override void UpdateQuest()
    {

    }

    public override void NextQuest()
    {
        Q_1_trigger.enabled = false;
        Q_2_trigger.enabled = true;

        // ����Ʈ UI�� 3�ʰ� ǥ��
        StartCoroutine(ShowQuestUI());
    }

    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI Ȱ��ȭ
        yield return new WaitForSeconds(3); // 3�� ���
        questUI.SetActive(false); // UI ��Ȱ��ȭ
    }
}