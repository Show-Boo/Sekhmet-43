using UnityEngine;

public class Quest1_MoveToLocation : MonoBehaviour
{
    public GameObject cutscene1;  // 활성화할 cutscene1 오브젝트
    public GameObject cutscene2;  // cutscene1의 트리거 박스에 감지되면 활성화할 cutscene2 오브젝트
    public QuestManager questManager;  // 퀘스트 매니저 참조
    public Collider quest1Trigger;  // cutscene1 내부의 Quest1Trigger 트리거 영역

    private void Start()
    {
        // quest1Trigger가 설정되었는지 확인
        if (quest1Trigger == null)
        {
            Debug.LogError("Quest1Trigger is not assigned! Please assign the trigger collider from cutscene1.");
        }
        else
        {
            // Quest1Trigger의 트리거 이벤트를 연결
            quest1Trigger.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        Debug.Log("Quest 1 completed!");

        if (questManager != null)
        {
            questManager.CompleteObjective(1, "Move to Location");

            // cutscene1 오브젝트 활성화
            if (cutscene1 != null)
            {
                cutscene1.SetActive(true);
                Debug.Log("cutscene1 has been activated.");
            }
            else
            {
                Debug.LogError("cutscene1 is not assigned in the Inspector!");
                return;
            }

            // Quest1Trigger에서 트리거 이벤트 감지를 위한 추가 콜백 등록
            quest1Trigger.gameObject.AddComponent<Quest1TriggerHandler>().Initialize(this);
        }
        else
        {
            Debug.LogError("QuestManager is not assigned in the Inspector!");
        }
    }

    // Quest1Trigger가 플레이어와 충돌했을 때 호출될 메서드
    public void ActivateCutscene2()
    {
        if (cutscene2 != null)
        {
            cutscene2.SetActive(true);
            Debug.Log("cutscene2 has been activated.");
        }
        else
        {
            Debug.LogError("cutscene2 is not assigned in the Inspector!");
        }
    }
}

public class Quest1TriggerHandler : MonoBehaviour
{
    private Quest1_MoveToLocation quest1_MoveToLocation;

    public void Initialize(Quest1_MoveToLocation quest)
    {
        quest1_MoveToLocation = quest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quest1_MoveToLocation.ActivateCutscene2();
        }
    }
}
