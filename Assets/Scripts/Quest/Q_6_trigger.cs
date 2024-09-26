using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_6_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Q_6 q_6;
    public Q_7 q_7;
    public GeneratorManager generatorManager;
    //public QuestManager QuestManager;
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
                    
                    if (!q_6.Quest6_clear)//퀘스트 6 클리어 전
                    {
                        // 조건을 만족할 때 불 값 true로 변경
                        q_6.Quest6_clear = true;
                        Debug.Log("Engine hit and boolean set to true!");
                    }
                    else if (generatorManager.engineIsAllFixed)//엔진이 다 고쳐진 경우
                    {
                        q_7.q_7_done = true;//클릭하면 퀘스트 클리어..
                        generatorManager.RepairGenerator();//불 키고 엔진 돌리기
                    }

                }
                else if (hit.collider.CompareTag("Spaceship"))
                {
                    if (q_6.Quest6_clear && q_7.q_7_done)//퀘스트 7까지 성공한 경우
                    {

                    }
                }
            }
        }
    }
}
