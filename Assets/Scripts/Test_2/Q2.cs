using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q2 : Q
{
    public GameObject item;
    public override bool Cutscene { get; set; } = false;
    public bool tagPlayer = false;

    public GameObject text;
    public override void UpdateQuest()
    {
        if (tagPlayer)
        {
            CompleteQuest(); // 아이템이 없으면 퀘스트 완료 처리
        }
        else
        {
            Debug.Log("Quest 2...");
        }
    }

    public override void NextQuest()
    {
        text.SetActive(true);
    }

}
