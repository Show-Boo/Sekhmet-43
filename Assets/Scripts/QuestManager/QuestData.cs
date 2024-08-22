using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int questID;  // 퀘스트 ID
    public string questName;  // 퀘스트 이름
    public string description;  // 퀘스트 설명
    public bool isCompleted;  // 퀘스트 완료 여부

    // 요구 사항들 (예: 특정 아이템 수집, 특정 장소 도달 등)
    public List<string> objectives = new List<string>();

    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log("Quest " + questName + " completed!");
        // 보상 지급 또는 다음 퀘스트 활성화 코드 추가
    }
}
