using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    //public QManager questManager;  // ����Ʈ �Ŵ��� ����
    //public int questIndex;             // �� �������� �Ϸ��� ����Ʈ �ε���

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //questManager.CompleteObjective(questIndex);
            Destroy(gameObject);  // ������ ȹ�� �� ����. �̷��� null==true�� ��
        }
    }
}
