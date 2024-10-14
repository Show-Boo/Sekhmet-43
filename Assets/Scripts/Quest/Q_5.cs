using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Q_5 : Q_ParentClass
{
    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    public OpenDoor OpenDoor;
    public GameObject Quest_6_Text;//gameObject로 받아오는 경우, 그냥 작동은 하긴 하겠지만 getcomponent를 쓰는게 더 안전. 혹은 Text로 받아오는 것도 안전함.
    public Start_myproject start_Myproject;
    public GameObject questUI; // 퀘스트 UI를 나타내는 GameObject 추가

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
        //Quest_6_Text.SetActive(true); 얘는 되는디>
        //ShowMessage(Quest_6_Text, 3.0f);//이게 안먹힘...하 이유가 뭐지
        Quest_6_Text.SetActive(true);
        Debug.Log("1");

        StartCoroutine(ShowQuestUI());
    }

    public IEnumerator ShowMessage(GameObject message, float delay)
    {
        message.SetActive(true); // 텍스트 보이게 하기
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        Debug.Log("message set active");
        message.SetActive(false); // 텍스트 숨기기
    }

    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI 활성화
        yield return new WaitForSeconds(3); // 3초 대기
        questUI.SetActive(false); // UI 비활성화
    }


}
