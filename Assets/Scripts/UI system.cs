using UnityEngine;

public class DisplayUIOnApproach : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트
    public GameObject textUI;  // 텍스트 UI 오브젝트
    public float triggerDistance = 5.0f;  // UI가 표시될 거리

    void Update()
    {
        // 플레이어와 문(이 스크립트가 붙어 있는 오브젝트)의 거리 계산
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // 플레이어가 일정 거리 내에 들어오면 UI 활성화
        if (distance <= triggerDistance)
        {
            textUI.SetActive(true);
        }
        else
        {
            textUI.SetActive(false);
        }
    }
}


