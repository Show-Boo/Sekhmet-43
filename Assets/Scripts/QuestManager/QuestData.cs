using System;
using UnityEngine;

[Serializable]
public class Quest
{
    public string questName;
    public string description;
    public GameObject[] activationTriggers;   // 퀘스트 시작 시 활성화할 트리거 박스들
    public GameObject[] deactivationTriggers; // 퀘스트 완료 시 비활성화할 트리거 박스들
    public Action onStart;                    // 퀘스트 시작 시 호출될 이벤트
    public Action onComplete;                 // 퀘스트 완료 시 호출될 이벤트
    public string videoPath;                  // 퀘스트와 연결된 동영상 파일 경로
    public bool isCompleted;
}
