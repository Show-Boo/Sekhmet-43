using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_1_trigger : MonoBehaviour
{
    public QuestManager QuestManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // QuestManager.CompleteObjective(0); // ù ��° ����Ʈ �Ϸ�. ==> ������ �������� �ϴ� �ּ�ó����.
        }
    }
}
