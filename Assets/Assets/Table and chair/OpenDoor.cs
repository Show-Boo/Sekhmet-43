using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation hingehere;
    public bool isOpen = false;

    // 플레이어의 ActionController를 참조
    public ActionController playerActionController;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            if (playerActionController != null && playerActionController.CanOpenDoor())
            {
                hingehere.Play();
                isOpen = true;
                Debug.Log("문이 열렸습니다!");
            }
            else
            {
                Debug.Log("문을 열기 위한 아이템이 없습니다.");
            }
        }
    }
}
