using UnityEngine;

public class DisplayUIOnApproach : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트
    public GameObject textUI;  // 텍스트 UI 오브젝트
    public float triggerDistance = 5.0f;  // UI가 표시될 거리

    public bool isFirst = false; //처음 player가 닿는 순간 

    public OpenDoor openDoor;
    void Update()
    {
        if (!openDoor.isOpen) //열리기 전에만 뜨게
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // 플레이어가 일정 거리 내에 들어오면 UI 활성화
            if (distance <= triggerDistance)
            {
                textUI.SetActive(true);
                if (!isFirst)
                {
                    isFirst = true;
                }
            }
            else
            {
                textUI.SetActive(false);
            }
        }
        else //열리고나서
        {
            textUI.SetActive(false);
        }
       
        
        
    }
}


