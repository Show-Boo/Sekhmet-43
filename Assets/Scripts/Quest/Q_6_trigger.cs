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

        // Ray�� ����
        float rayLength = 5f;
        
        // ���콺 Ŭ�� Ȯ��
        if (Input.GetMouseButtonDown(0)) // 0�� ���� ���콺 ��ư
        {
            // Raycast�� ��� �浹�� ������Ʈ�� �ִ��� Ȯ��
            if (Physics.Raycast(transform.position, forward, out RaycastHit hit, rayLength))
            {
                // �浹�� ������Ʈ�� "engine" �±װ� �Ǿ��ִ��� Ȯ��
                if (hit.collider.CompareTag("Engine"))
                {
                    
                    if (!q_6.Quest6_clear)//����Ʈ 6 Ŭ���� ��
                    {
                        // ������ ������ �� �� �� true�� ����
                        q_6.Quest6_clear = true;
                        Debug.Log("Engine hit and boolean set to true!");
                    }
                    else if (generatorManager.engineIsAllFixed)//������ �� ������ ���
                    {
                        q_7.q_7_done = true;//Ŭ���ϸ� ����Ʈ Ŭ����..
                        generatorManager.RepairGenerator();//�� Ű�� ���� ������
                    }

                }
                else if (hit.collider.CompareTag("Spaceship"))
                {
                    if (q_6.Quest6_clear && q_7.q_7_done)//����Ʈ 7���� ������ ���
                    {

                    }
                }
            }
        }
    }
}
