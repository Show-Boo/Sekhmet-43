using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager questManager;

    void Start()
    {
        // 퀘스트 매니저 찾기
        questManager = FindObjectOfType<QuestManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거 박스에 들어왔을 때
        if (other.CompareTag("Player"))
        {
            questManager.CompleteCurrentQuest();
            gameObject.SetActive(false); // 트리거 박스 비활성화
        }
    }
}