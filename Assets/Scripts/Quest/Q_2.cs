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

    public GameObject questUI; // ����Ʈ UI�� ��Ÿ���� GameObject �߰�

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
