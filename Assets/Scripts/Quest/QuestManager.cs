using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //0913 여기 코드는 수정이 더 필요할듯
    // Start is called before the first frame update
    public Q_ParentClass[] Quest;//퀘스트배열
    private int currentQuestIndex = 0;//현재 진행중인 퀘스트 인덱스

    void Start()
    {
        StartQuest(currentQuestIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        // 현재 퀘스트가 활성화되어 있다면 해당 퀘스트에 대한 업데이트를 실행
        if (Quest[currentQuestIndex].isActive)
        {
            Quest[currentQuestIndex].UpdateQuest();
        }
    }

    public void StartQuest(int questIndex)
    {
        if (questIndex < Quest.Length)
        {
            Debug.Log($"Starting Quest: {Quest[questIndex].questName}");
            Quest[questIndex].isActive = true;
        }
    }

    public void CompleteObjective(int questIndex)
    {
        if (questIndex == currentQuestIndex && Quest[questIndex].isActive)
        {
            Quest[questIndex].CompleteQuest();
            currentQuestIndex++;

            // 다음 퀘스트가 있다면 시작
            if (currentQuestIndex < Quest.Length)
            {
                StartQuest(currentQuestIndex);
            }
        }
        else//퀘스트가 끝난 경우에 이 오류로 빠질수도..?아마..
        {
            Debug.LogWarning($"Quest {questIndex} cannot be completed yet. Complete the current quest first.");
        }
    }
    

}
