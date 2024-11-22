using UnityEngine;

public class DoorOpenUI : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트
    public GameObject textUI1;  // 처음 접근 시 표시할 텍스트 UI 오브젝트
    public GameObject textUI2;  // 이후 접근 시 표시할 텍스트 UI 오브젝트
    public float triggerDistance = 5.0f;  // UI가 표시될 거리
    public OpenDoor openDoor; // 문 오브젝트의 상태를 확인
    public ActionController actionController; // 카드키 상태를 확인하는 ActionController 참조

    public bool isFirst = false; // 플레이어가 처음 접근했는지 확인

    private void Start()
    {
        // 처음에는 두 UI 모두 비활성화
        if (textUI1 != null) textUI1.SetActive(false);
        if (textUI2 != null) textUI2.SetActive(false);

        // ActionController가 연결되지 않은 경우 경고 메시지
        if (actionController == null)
        {
            Debug.LogError("ActionController가 설정되지 않았습니다.");
        }
    }

    private void Update()
    {
        // 문이 열려 있으면 모든 UI를 표시하지 않음
        if (openDoor != null && openDoor.isOpen)
        {
            if (textUI1 != null) textUI1.SetActive(false);
            if (textUI2 != null) textUI2.SetActive(false);
            return;
        }

        // 플레이어와의 거리 계산
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // 플레이어가 일정 거리 내에 있으면 UI 표시
        if (distance <= triggerDistance)
        {
            if (!isFirst) // 처음 접근 시
            {
                if (textUI1 != null) textUI1.SetActive(true); // "카드키가 필요합니다" 표시
                if (textUI2 != null) textUI2.SetActive(false);

                Debug.Log("플레이어가 처음 접근했습니다.");
                isFirst = true; // 처음 접근 상태로 변경
            }
            else // 이후 접근 시
            {
                if (actionController != null && actionController.CanOpenDoor())
                {
                    if (textUI1 != null) textUI1.SetActive(false);
                    if (textUI2 != null) textUI2.SetActive(true); // "E 키를 눌러 문을 여십시오" 표시
                }
                else
                {
                    if (textUI1 != null) textUI1.SetActive(true); // 카드키가 없으면 처음 메시지 재표시
                    if (textUI2 != null) textUI2.SetActive(false);
                }
            }
        }
        else
        {
            // 플레이어가 일정 거리 밖에 있으면 UI 비활성화
            if (textUI1 != null) textUI1.SetActive(false);
            if (textUI2 != null) textUI2.SetActive(false);
        }
    }
}
