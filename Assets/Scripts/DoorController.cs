
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform rotatePoint; // ȸ�� �� ����Ʈ
    public GameObject door; // �� ������Ʈ
    public float rotationAngle = 90f; // ȸ�� ����
    public float rotationDuration = 2f; // ȸ�� �ð�
    public GameObject openDoorText; // UI �ؽ�Ʈ ������Ʈ

    private bool isOpen = false;
    private bool isPlayerNear = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

   void Start()
    {
        // ���� ������ ���� ȸ�� ���� ����
        closedRotation = door.transform.rotation;
        openRotation = Quaternion.Euler(0, rotationAngle, 0) * closedRotation;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            StopAllCoroutines();
            StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
            Debug.Log("E key pressed, door should open/close.");
        }
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Quaternion startRotation = door.transform.rotation;
        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            door.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = targetRotation; // ȸ���� ��Ȯ�� ��ǥ ȸ�������� ����
        Debug.Log("Door rotation completed.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player entered the trigger area.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called");
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player exited the trigger area.");
        }
    }
}
