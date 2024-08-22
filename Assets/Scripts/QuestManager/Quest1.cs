using UnityEngine;

public class Quest1_MoveToLocation : MonoBehaviour
{
    public GameObject cutscene1;  // 활성화할 cutscene1 오브젝트
    public QuestManager questManager;  // 퀘스트 매니저 참조

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
            }
        }
        else
        {
            Debug.LogError("QuestManager is not assigned in the Inspector!");
        }
    }
}
