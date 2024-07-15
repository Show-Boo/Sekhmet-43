using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo 1. 활성화 된 hiding 카메라만 움직이기
//Todo 2. 숨었다가 복귀
//3. 근거리에서는 숨어도 공격
//4. 좀비 시작 직후에는 움직이지 않게
//5.

public class PlayerHiding : MonoBehaviour
{
    //public Transform[] objects;

    //public Transform Player1; // Player1의 Transform
    //public Transform HidePlayer; // Player2의 Transform
    //public Transform HidePlayer2;

    //private Transform CurrentTransform;

    //private Camera PlayerCamera; // Player1의 카메라
    //private Camera HidePlayerCamera; // Player2의 카메라
    //private Camera HidePlayer2Camera;

    private Camera CurrentCamera;
    
    //public float switchDistance = 5.0f; // 거리 임계값
    public bool isPlayer1Active = true; // 현재 활성화된 플레이어 여부

    private Enemy[] enemyMove;

    //Raycast
    public Camera playerCamera; // 플레이어의 카메라
    public float checkDistance = 5.0f; // 체크할 거리
    public LayerMask layerMask; // 충돌을 감지할 레이어 마스크
    public string interactableTag = "InteractiveObject"; // 상호작용할 태그

    //private Camera targetCamera;

    void Start()
    {
        // 시작 시 Player1 카메라 활성화, Player2 카메라 비활성화
        /*
        PlayerCamera = Player1.GetComponentInChildren<Camera>();
        HidePlayerCamera = HidePlayer.GetComponentInChildren<Camera>();
        HidePlayer2Camera = HidePlayer2.GetComponentInChildren<Camera>();

        
        PlayerCamera.gameObject.SetActive(true);
        HidePlayerCamera.gameObject.SetActive(false);
        HidePlayer2Camera.gameObject.SetActive(false);
        */

        enemyMove = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        // 플레이어 간 거리 계산
        /*
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
        */
        if (isPlayer1Active)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, checkDistance, layerMask))
            {
                // 일정 거리 내로 들어오는 객체가 감지되면
                if (hit.collider.CompareTag(interactableTag))
                {
                    Debug.Log("Interactable object within range: " + hit.collider.name);

                    CurrentCamera = hit.collider.GetComponentInChildren<Camera>();

                    if (CurrentCamera != null && Input.GetKeyDown(KeyCode.Q))
                    {
                        // Q키가 눌리면 카메라 전환
                        SwitchCamera();

                    }

                    else if (CurrentCamera == null)
                    {

                        Debug.Log("There is no Camera");

                    }
                }
            }

            else
            {
                // 일정 거리 내에 감지된 객체가 없을 때
                Debug.Log("No interactable object within range.");
            }


        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Q)) { 

                SwitchCamera();
            }
        }
        
    }

    void SwitchCamera()
    {
        // null 체크
        //CurrentCamera = CurrentTransform.GetComponent<Camera>();//GetComponentInChildren하면 못받아와서 걍 obj에 카메라 넣어줌

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
            playerCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = CurrentCamera;//enemy의 타켓 바꿔주기
            }
        }

        else
        {
            // Player1 카메라 활성화, Player2 카메라 비활성화
            playerCamera.gameObject.SetActive(true);
            CurrentCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = playerCamera;//enemy의 타켓 바꿔주기
            }

        }
        /*
        foreach (var enemyMoveScript in enemyMove)
        {
            enemyMoveScript.ActivatedCamera = CurrentCamera;//enemy의 타켓 바꿔주기
        }
        */

        // 활성화된 플레이어 상태 업데이트
        isPlayer1Active = !isPlayer1Active;

        Debug.Log("isPlayer1Active: " + isPlayer1Active);

    }

    void OnDrawGizmos()
    {
        // 디버그를 위해 레이캐스트 경로를 씬 뷰에 그려줌
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * checkDistance);
        }

    }
}



