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
                    // ������ ������ �� �� �� true�� ����
                    q_6.Quest6_clear = true;
                    Debug.Log("Engine hit and boolean set to true!");
                }
            }
        }
    }
}
