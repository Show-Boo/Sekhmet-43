using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKeyCard = false; // 카드키 보유 여부를 추적하는 변수

    // 열려있는 문을 나타내는 변수
    public GameObject door;
    private bool doorOpen = false;

    void Update()
    {
        // "E" 키를 누르면 문을 열거나 닫기
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpen)
            {
                CloseDoor();
            }
            else
            {
                // 문이 닫혀있고 카드키를 가지고 있다면 문을 열기
                if (hasKeyCard)
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("카드키가 필요합니다!");
                }
            }
        }
    }

    // 문을 열기
    void OpenDoor()
    {
        door.SetActive(false); // 문을 열어야 하지만 여기서는 비활성화로 대체
        doorOpen = true;
        Debug.Log("문이 열렸습니다!");
    }

    // 문을 닫기
    void CloseDoor()
    {
        door.SetActive(true); // 문을 닫아야 하지만 여기서는 활성화로 대체
        doorOpen = false;
        Debug.Log("문이 닫혔습니다.");
    }
}

