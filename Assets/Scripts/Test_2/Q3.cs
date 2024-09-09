using UnityEngine;

public class ButtonQuestManager : MonoBehaviour // 퀘스트9 버튼 눌러 우주선 작동
{
    private bool playerInRange = false; // 플레이어가 버튼 범위 내에 있는지 확인
    private bool questCompleted = false; // 퀘스트 완료 여부

    void Update()
    {
        // 플레이어가 버튼 범위 내에 있고 E 키를 누르면 퀘스트 완료
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !questCompleted)
        {
            CompleteQuest();
        }
    }

    // 퀘스트 완료 처리
    private void CompleteQuest()
    {
        questCompleted = true;
        Debug.Log("퀘스트 완료!");
        // 퀘스트 완료 후 추가 작업 (예: UI 표시, 보상 지급 등)
    }

    // 플레이어가 버튼 범위 내로 들어왔을 때
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("플레이어가 버튼 범위 내로 들어왔습니다.");
        }
    }

    // 플레이어가 버튼 범위 밖으로 나갔을 때
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("플레이어가 버튼 범위를 벗어났습니다.");
        }
    }
}
