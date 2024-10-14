using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Q_5 : Q_ParentClass
{
    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public OpenDoor OpenDoor;
    public GameObject Quest_6_Text;//gameObject�� �޾ƿ��� ���, �׳� �۵��� �ϱ� �ϰ����� getcomponent�� ���°� �� ����. Ȥ�� Text�� �޾ƿ��� �͵� ������.
    public Start_myproject start_Myproject;
    public GameObject questUI; // ����Ʈ UI�� ��Ÿ���� GameObject �߰�

    public override void UpdateQuest()
    {
        if (OpenDoor.isOpen)
        {
            QuestManager.CompleteObjective();
        }
    }

    // Update is called once per frame
    public override void NextQuest()
    {
        //start_Myproject.ShowMessage(Quest_6_Text, 3.0f);
        //Text text = Quest_6_Text.GetComponent<Text>();
        //text.enabled = false;
        //Quest_6_Text.SetActive(true); ��� �Ǵµ�>
        //ShowMessage(Quest_6_Text, 3.0f);//�̰� �ȸ���...�� ������ ����
        Quest_6_Text.SetActive(true);
        Debug.Log("1");

        StartCoroutine(ShowQuestUI());
    }

    public IEnumerator ShowMessage(GameObject message, float delay)
    {
        message.SetActive(true); // �ؽ�Ʈ ���̰� �ϱ�
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        Debug.Log("message set active");
        message.SetActive(false); // �ؽ�Ʈ �����
    }

    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI Ȱ��ȭ
        yield return new WaitForSeconds(3); // 3�� ���
        questUI.SetActive(false); // UI ��Ȱ��ȭ
    }


}
