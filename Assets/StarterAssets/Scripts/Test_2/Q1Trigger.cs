using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q1Trigger : MonoBehaviour
{
    public QManager questManager;  // ����Ʈ �Ŵ��� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questManager.CompleteObjective(0); // ù ��° ����Ʈ �Ϸ�.
        }
    }
}
