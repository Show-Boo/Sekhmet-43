using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingRaycast : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera playerCamera; // 플레이어의 카메라
    public float checkDistance = 10.0f; // 체크할 거리
    public LayerMask layerMask; // 충돌을 감지할 레이어 마스크
    public string interactableTag = "InteractiveObject"; // 상호작용할 태그

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, checkDistance, layerMask))
        {
            // 일정 거리 내로 들어오는 객체가 감지되면
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Interactable object within range: " + hit.collider.name);
                // 여기서 작업 수행
            }
        }

        else
        {
            // 일정 거리 내에 감지된 객체가 없을 때
            Debug.Log("No interactable object within range.");
        }

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
