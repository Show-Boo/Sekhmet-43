using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    //public QManager questManager;  // 퀘스트 매니저 참조
    //public int questIndex;             // 이 아이템이 완료할 퀘스트 인덱스

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //questManager.CompleteObjective(questIndex);
            Destroy(gameObject);  // 아이템 획득 후 제거. 이래도 null==true가 됨
        }
    }
}
