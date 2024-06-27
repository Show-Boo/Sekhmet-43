using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{

    public Transform Player1; // Player1의 Transform
    public Transform HidePlayer; // Player2의 Transform
    private Camera PlayerCamera; // Player1의 카메라
    private Camera HidePlayerCamera; // Player2의 카메라
    public float switchDistance = 5.0f; // 거리 임계값

    public bool isPlayer1Active = true; // 현재 활성화된 플레이어 여부

    void Start()
    {
        // 시작 시 Player1 카메라 활성화, Player2 카메라 비활성화
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);

        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // 플레이어 간 거리 계산
        float distance = Vector3.Distance(Player1.position, HidePlayer.position);

        if (Input.GetKeyDown(KeyCode.R) && distance <= switchDistance)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isPlayer1Active)
        {
            // Player2 카메라 활성화, Player1 카메라 비활성화
            PlayerCamera.gameObject.SetActive(false);
            HidePlayerCamera.gameObject.SetActive(true);
        }
        else
        {
            // Player1 카메라 활성화, Player2 카메라 비활성화
            PlayerCamera.gameObject.SetActive(true);
            HidePlayerCamera.gameObject.SetActive(false);
        }

        // 활성화된 플레이어 상태 업데이트
        isPlayer1Active = !isPlayer1Active;
    }

}

