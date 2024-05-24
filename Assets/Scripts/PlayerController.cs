using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isHiding = false;
    private Collider currentHidingSpot = null; //공간 추적
    public float enterDistance = 3f; // 공간 근처에서 얼마나 가까워야 하는지 설정
    private Vector3 originalPosition; // 숨기 전 플레이어 위치 저장

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
                originalPosition = transform.position; // 현재 플레이어 위치 저장
                transform.position = hidingSpotPosition; // 플레이어 위치 이동
                isHiding = true;
            }
        }
    }

    private void ExitHidingSpot()
    {
        transform.position = originalPosition; // 플레이어를 원래 위치로 이동
        isHiding = false;
    }
}
