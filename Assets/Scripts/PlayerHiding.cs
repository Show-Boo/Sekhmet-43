using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public Transform[] objects;

    public Transform Player1; // Player1의 Transform
    public Transform HidePlayer; // Player2의 Transform
    public Transform HidePlayer2;

    private Transform CurrentTransform;

    private Camera PlayerCamera; // Player1의 카메라
    private Camera HidePlayerCamera; // Player2의 카메라
    private Camera HidePlayer2Camera;

    private Camera CurrentCamera;
    
    public float switchDistance = 5.0f; // 거리 임계값
    public bool isPlayer1Active = true; // 현재 활성화된 플레이어 여부

    //private Enemy enemyMove;

    private Enemy[] enemyMove;
    void Start()
    {
        // 시작 시 Player1 카메라 활성화, Player2 카메라 비활성화
        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
        HidePlayer2Camera = HidePlayer2.GetComponentInChildren<Camera>();

        
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);
        HidePlayer2Camera.gameObject.SetActive(false);

        enemyMove = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        // 플레이어 간 거리 계산

        float distance = float.MaxValue;

        // 모든 객체와의 거리를 비교
        foreach (Transform obj in objects)
        {
            float currentDistance = Vector3.Distance(Player1.position, obj.position);

            if (currentDistance < distance)
            {
                distance = currentDistance;
                CurrentTransform = obj;
            }
        }
        //float distance = Vector3.Distance(Player1.position, HidePlayer.position);

        if (Input.GetKeyDown(KeyCode.R) && distance <= switchDistance)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // null 체크
        CurrentCamera = CurrentTransform.GetComponent<Camera>();//GetComponentInChildren하면 못받아와서 걍 obj에 카메라 넣어줌

        /*
        if (CurrentTransform != null)
        {
            Debug.LogError(CurrentTransform);
            return;
        }
        문제없음
        */

        if (CurrentCamera == null)
        {
            Debug.LogError("CurrentCamera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)
        {
            // Player2 카메라 활성화, Player1 카메라 비활성화
            CurrentCamera.gameObject.SetActive(true);//활성화부터 시켜주기.. 이유는 모르겠는데 그래야 렌더링 가능
            PlayerCamera.gameObject.SetActive(false);
            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;
            }

        }
        else
        {
            // Player1 카메라 활성화, Player2 카메라 비활성화
            PlayerCamera.gameObject.SetActive(true);
            CurrentCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;
            }

        }

        // 활성화된 플레이어 상태 업데이트
        isPlayer1Active = !isPlayer1Active;
        Debug.Log("isPlayer1Active: " + isPlayer1Active);

    }
}



