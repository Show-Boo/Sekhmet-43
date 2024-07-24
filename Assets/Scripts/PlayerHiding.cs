using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo 1. 활성화 된 hiding 카메라만 움직이기 -> done
//Todo 2. 숨었다가 복귀 -> done
//3. 근거리에서는 숨어도 공격 -> done 근데 player 공격중일때는 숨은 쪽으로 안옴
//3-2 공격중일때도 player 위치로 오게
//4. 좀비 시작 직후에는 움직이지 않게
//5. 같은 위치로 두번 이상 숨을 수 있게 : 카메라 비활성화 되어도 접근하게 or 비활성화 안되게,,,,,,,,,,,,,,,,,, -> done


//0717 : 책상 밑 보고 q 누르면 옷장에 숨어짐,,감지는 잘 함, 돌아가는 것도 안됨, -> 초기에 모든 카메라가 활성화되어있는 탓인듯 -> 초기 비활성화로 해결함
// : 옷장만 no rendering camera 라고 뜸..흑흑
// : 카메라 옆의 체크박스를 체크했더니 문제 해결ㅎㅎ

public class PlayerHiding : MonoBehaviour
{
    private Camera CurrentCamera;
    private Camera previousCamera; // 이전 카메라를 저장할 변수

    public bool isPlayer1Active = true; // 현재 활성화된 플레이어 여부

    private EnemyMove[] enemyMove;

    //Raycast
    public Camera playerCamera; // 플레이어의 카메라
    public float checkDistance = 5.0f; // 체크할 거리
    public LayerMask layerMask; // 충돌을 감지할 레이어 마스크
    public string interactableTag = "InteractiveObject"; // 상호작용할 태그

    void Start()
    {
        enemyMove = FindObjectsOfType<EnemyMove>();

        previousCamera = playerCamera;
    }

    void Update()
    {
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

                    CurrentCamera = hit.collider.GetComponentInChildren<Camera>(true); // true 설정하면 비활성화된 객체도 감지

                    if (CurrentCamera != null)
                    {
                        Debug.Log("CurrentCamera: " + CurrentCamera.name);

                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            // Q키가 눌리면 카메라 전환
                            SwitchCamera();
                        }
                    }

                    else//두번째부터는 카메라 감지를 못함..왜???????? -> 비활성화 된 카메라는 찾아내지 못함
                    {
                        Debug.Log("There is no Camera on the interactable object.");
                    }
                }
            }

            else
            {
                CurrentCamera = null;
                // 일정 거리 내에 감지된 객체가 없을 때
                Debug.Log("No interactable object within range.");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (previousCamera != null)
                {
                    CurrentCamera = previousCamera;
                    SwitchCamera();
                }
                else
                {
                    Debug.LogError("Previous camera is null!");
                }
            }
        }
    }

    void SwitchCamera()
    {
        if (CurrentCamera == null)
        {
            Debug.LogError("CurrentCamera component is null! Make sure cameras are assigned.");
            return;
        }

        if (isPlayer1Active)
        {
            // 다른 오브젝트의 카메라를 활성화하고 플레이어 카메라를 비활성화
            if (CurrentCamera != null)
            {
                CurrentCamera.gameObject.SetActive(true);
                

                Debug.Log("Switched to CurrentCamera: " + CurrentCamera.name);
            }

            playerCamera.gameObject.SetActive(false);

            foreach (var enemyMoveScript in enemyMove)
            {

                enemyMoveScript.ActivatedCamera = CurrentCamera;

            }

            
            previousCamera = CurrentCamera;

        }
        else
        {
            // 플레이어 카메라를 활성화하고 다른 오브젝트의 카메라를 비활성화

            playerCamera.gameObject.SetActive(true);

            if (previousCamera != null)
            {

                previousCamera.gameObject.SetActive(false);
                Debug.Log("Switched to PlayerCamera.");

            }

            foreach (var enemyMoveScript in enemyMove)
            {
                enemyMoveScript.ActivatedCamera = playerCamera;
            }

            previousCamera = playerCamera;
        }

        // 활성화된 플레이어 상태 업데이트
        isPlayer1Active = !isPlayer1Active;

        Debug.Log("isPlayer1Active: " + isPlayer1Active);
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane));
            Gizmos.DrawRay(rayOrigin, playerCamera.transform.forward * checkDistance);

        }
    }
}