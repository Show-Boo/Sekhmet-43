using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isHiding = false;
    private Collider currentHidingSpot = null; //���� ����
    public float enterDistance = 3f; // ���� ��ó���� �󸶳� ������� �ϴ��� ����
    private Vector3 originalPosition; // ���� �� �÷��̾� ��ġ ����

    void Update()
    {
        if (currentHidingSpot != null && Input.GetKeyDown(KeyCode.R))
        {
            if (!isHiding)
            {
                EnterHidingSpot();
            }
            else
            {
                ExitHidingSpot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            currentHidingSpot = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            if (currentHidingSpot == other)
            {
                currentHidingSpot = null;
            }
        }
    }

    private void EnterHidingSpot()
    {
        if (currentHidingSpot != null)
        {
            Vector3 hidingSpotPosition = currentHidingSpot.transform.position;
            float distanceToHidingSpot = Vector3.Distance(transform.position, hidingSpotPosition);
            if (distanceToHidingSpot <= enterDistance)
            {
                originalPosition = transform.position; // ���� �÷��̾� ��ġ ����
                transform.position = hidingSpotPosition; // �÷��̾� ��ġ �̵�
                isHiding = true;
            }
        }
    }

    private void ExitHidingSpot()
    {
        transform.position = originalPosition; // �÷��̾ ���� ��ġ�� �̵�
        isHiding = false;
    }
}
