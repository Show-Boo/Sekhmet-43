using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Q[] q;//퀘스트배열
    private int currentQIndex = 0;//현재 진행중인 퀘스트 인덱스

    void Start()
    {
        StartQ(currentQIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        // 현재 퀘스트가 활성화되어 있다면 해당 퀘스트에 대한 업데이트를 실행
        if (q[currentQIndex].isActive)
        {
            q[currentQIndex].UpdateQuest();
        }
    }

    public void StartQ(int questIndex)
    {
        if (questIndex < q.Length)
        {
            Debug.Log($"Starting Quest: {q[questIndex].questName}");
            q[questIndex].isActive = true;
        }
    }

    public void CompleteObjective(int questIndex)
    {
        if (questIndex == currentQIndex && q[questIndex].isActive)
        {
            q[questIndex].CompleteQuest();
            currentQIndex++;

            // 다음 퀘스트가 있다면 시작
            if (currentQIndex < q.Length)
            {
                StartQ(currentQIndex);
            }
        }
        else
        {
            Debug.LogWarning($"Quest {questIndex} cannot be completed yet. Complete the current quest first.");
        }
    }

}
