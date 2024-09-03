using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q2 : Q
{
    public GameObject item;  // 퀘스트 아이템 오브젝트
    public override bool cutscene { get; set; } = false;


    public override void UpdateQuest()
    {
        if (item == null)
        {
            CompleteQuest(); // 아이템이 없으면 퀘스트 완료 처리
        }
    }

}
