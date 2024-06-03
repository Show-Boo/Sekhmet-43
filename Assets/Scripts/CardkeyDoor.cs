using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKeyCard = false; // ī��Ű ���� ���θ� �����ϴ� ����

    // �����ִ� ���� ��Ÿ���� ����
    public GameObject door;
    private bool doorOpen = false;

    void Update()
    {
        // "E" Ű�� ������ ���� ���ų� �ݱ�
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpen)
            {
                CloseDoor();
            }
            else
            {
                // ���� �����ְ� ī��Ű�� ������ �ִٸ� ���� ����
                if (hasKeyCard)
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("ī��Ű�� �ʿ��մϴ�!");
                }
            }
        }
    }

    // ���� ����
    void OpenDoor()
    {
        door.SetActive(false); // ���� ����� ������ ���⼭�� ��Ȱ��ȭ�� ��ü
        doorOpen = true;
        Debug.Log("���� ���Ƚ��ϴ�!");
    }

    // ���� �ݱ�
    void CloseDoor()
    {
        door.SetActive(true); // ���� �ݾƾ� ������ ���⼭�� Ȱ��ȭ�� ��ü
        doorOpen = false;
        Debug.Log("���� �������ϴ�.");
    }
}

