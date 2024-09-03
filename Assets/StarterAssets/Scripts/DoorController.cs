
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform rotatePoint; // 회전 축 포인트
    public GameObject door; // 문 오브젝트
    public float rotationAngle = 90f; // 회전 각도
    public float rotationDuration = 2f; // 회전 시간
    public GameObject openDoorText; // UI 텍스트 오브젝트

    private bool isOpen = false;
    private bool isPlayerNear = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

   void Start()
    {
        // 문이 시작할 때의 회전 값을 저장
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

        door.transform.rotation = targetRotation; // 회전을 정확히 목표 회전값으로 맞춤
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
