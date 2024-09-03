using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q1Trigger : MonoBehaviour
{
    public QManager questManager;  // 퀘스트 매니저 참조
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questManager.CompleteObjective(0); // 첫 번째 퀘스트 완료.
        }
    }
}
