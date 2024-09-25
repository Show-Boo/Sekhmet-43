using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_6_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Q_6 q_6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Ray의 길이
        float rayLength = 5f;

        // 마우스 클릭 확인
        if (Input.GetMouseButtonDown(0)) // 0은 왼쪽 마우스 버튼
        {
            // Raycast를 쏘고 충돌한 오브젝트가 있는지 확인
            if (Physics.Raycast(transform.position, forward, out RaycastHit hit, rayLength))
            {
                // 충돌한 오브젝트가 "engine" 태그가 되어있는지 확인
                if (hit.collider.CompareTag("Engine"))
                {
                    // 조건을 만족할 때 불 값 true로 변경
                    q_6.Quest6_clear = true;
                    Debug.Log("Engine hit and boolean set to true!");
                }
            }
        }
    }
}
