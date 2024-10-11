using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation hingehere;
    public bool isOpen = false;

    // �÷��̾��� ActionController�� ����
    public ActionController playerActionController;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            if (playerActionController != null && playerActionController.CanOpenDoor())
            {
                hingehere.Play();
                isOpen = true;
                Debug.Log("���� ���Ƚ��ϴ�!");
            }
            else
            {
                Debug.Log("���� ���� ���� �������� �����ϴ�.");
            }
        }
    }
}
