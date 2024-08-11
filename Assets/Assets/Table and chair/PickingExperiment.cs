using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndDoorInteraction : MonoBehaviour
{
    public Item item; // 아이템 참조 (플레이어가 줍는 아이템)
    public Component doorcolliderhere; // 문에 연결된 콜라이더 참조
    public GameObject keygone; // 열쇠 오브젝트 참조
    public Inventory playerInventory; // 플레이어의 인벤토리

    // Start is called before the first frame update
    void Start()
    {
        // 초기화 작업 (필요하다면 추가)
    }

    // 플레이어가 트리거 범위 내에 머물고 있을 때 실행
    void OnTriggerStay(Collider other)
    {
        // 트리거에 들어온 객체가 플레이어인지 확인
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            // 아이템을 플레이어의 인벤토리에 추가
            

            // 문을 여는 콜라이더 활성화
            doorcolliderhere.GetComponent<BoxCollider>().enabled = true;

            // 열쇠 오브젝트를 비활성화 (사라지게 함)
            keygone.SetActive(false);
        }
    }

    // 아이템을 플레이어의 인벤토리에 추가하는 메서드
    
}


