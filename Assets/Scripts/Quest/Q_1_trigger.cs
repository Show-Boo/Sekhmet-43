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
            QuestManager.CompleteObjective(); // 첫 번째 퀘스트 완료. ==> 컴파일 에러나서 일단 주석처리함.
        }
    }
}
